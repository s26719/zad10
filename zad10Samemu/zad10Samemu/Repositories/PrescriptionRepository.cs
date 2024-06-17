using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using zad10Samemu.Context;
using zad10Samemu.DTO_s;
using zad10Samemu.Exceptions;
using zad10Samemu.Helpers;
using zad10Samemu.Models;

namespace zad10Samemu.Repositories;

public class PrescriptionRepository : IPrescriptionRepository
{
    private readonly MyDbContext _myDbContext;
    private readonly IConfiguration _configuration;

    public PrescriptionRepository(MyDbContext myDbContext, IConfiguration configuration)
    {
        _myDbContext = myDbContext;
        _configuration = configuration;
    }

    

    public async Task AddPrescriptionAsync(PrescriptionOutDto prescriptionOutDto)
    {
        
        using var transation = _myDbContext.Database.BeginTransaction();
        try
        {
            var maxIdPrescription = _myDbContext.Prescriptions.Max(p => p.IdPrescription) + 1;
            //sprawdzay czy pacjent istnieje
            var patient = prescriptionOutDto.PatientOutDto;
            var patientExist = await _myDbContext.Patients.FirstOrDefaultAsync(p => p.IdPatient == patient.IdPatient);

            if (patientExist == null)
            {
                var Patient = new Patient
                {
                    FirstName = patient.FirstName,
                    LastName = patient.LastName,
                    Birthdate = patient.Birthdate
                };
                await _myDbContext.Patients.AddAsync(Patient);
                await _myDbContext.SaveChangesAsync();
            }
            
            
            //sprawdzamy czy istenieje lek podany na recepcie
            foreach (var medicament in prescriptionOutDto.Medicaments)
            {
                var medicamentExist =
                    await _myDbContext.Medicaments.FirstOrDefaultAsync(m => m.IdMedicament == medicament.IdMedicament);
                if (medicamentExist== null)
                {
                    throw new NotFoundException("Lek o podanym Id nie istnieje");
                }
            }

            var Prescription = new Prescription
            {
                Date = prescriptionOutDto.Date,
                DueDate = prescriptionOutDto.DueDate,
                IdPatient = prescriptionOutDto.PatientOutDto.IdPatient,
                IdDoctor = 1
            };
            await _myDbContext.Prescriptions.AddAsync(Prescription);
            await _myDbContext.SaveChangesAsync();

            foreach (var medicament in prescriptionOutDto.Medicaments)
            {
               var  Prescription_medicament = new Prescription_Medicament
                {
                    IdMedicament = medicament.IdMedicament,
                    IdPrescription = maxIdPrescription,
                    Dose = medicament.Dose,
                    Details = medicament.Description
                };
                await _myDbContext.PrescriptionMedicaments.AddAsync(Prescription_medicament);
                await _myDbContext.SaveChangesAsync();
            }

            await transation.CommitAsync();
        }
        catch (NotFoundException e)
        {
            await transation.RollbackAsync();
            throw new NotFoundException(e.Message);
        }
        
    }

    public async Task RegisterUser(RegisterRequest model)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);
        
        var user = new AppUser()
        {
            Email = model.Email,
            Login = model.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };

        await _myDbContext.Users.AddAsync(user);
        await _myDbContext.SaveChangesAsync();
    }

    public async Task<TokenResponse> LoginUser(LoginRequest loginRequest)
    {
        AppUser user = await _myDbContext.Users.Where(u => u.Login == loginRequest.Login).FirstOrDefaultAsync();
    
        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);
    
        if (passwordHashFromDb != curHashedPassword)
        {
            return null;
        }
    
    
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "Franek"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
        };
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
    
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    
        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );
    
        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _myDbContext.SaveChangesAsync();
        
        return new TokenResponse(
            AccessToken: new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken: user.RefreshToken
            
        );
    }

    public async Task<TokenResponse> RefreshUser(RefreshTokenRequest refreshToken)
    {
        AppUser user = await _myDbContext.Users.Where(u => u.RefreshToken == refreshToken.RefreshToken).FirstOrDefaultAsync();
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }
    
        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }
        
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "Franek"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
        };
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
    
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
    
        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );
    
        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _myDbContext.SaveChangesAsync();
    
        return new TokenResponse(
            AccessToken: new JwtSecurityTokenHandler().WriteToken(jwtToken),
            RefreshToken: user.RefreshToken
        );
    }
}