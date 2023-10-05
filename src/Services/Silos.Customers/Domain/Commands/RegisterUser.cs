namespace Silos.Users.Domain.Commands;

public record class RegisterUser : ICommand
{
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string PasswordConfirm { get; private set; }
    public string Name { get; private set; }
    
    public static RegisterUser Create(
        string email,
        string password,
        string passwordConfirm,
        string name)
    {
        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException(nameof(email));
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));
        if (string.IsNullOrEmpty(passwordConfirm))
            throw new ArgumentNullException(nameof(passwordConfirm));
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));
       
        return new RegisterUser(email,
            password,
            passwordConfirm,
            name);
    }

    private RegisterUser(
        string email,
        string password,
        string passwordConfirm,
        string name)
    {
        Email = email;
        Password = password;
        PasswordConfirm = passwordConfirm;
        Name = name;
    }
}