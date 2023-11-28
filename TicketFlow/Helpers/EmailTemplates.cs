namespace TicketFlow.Helpers;

public class EmailTemplates
{
    private static string OtenerPlantillaHtml()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "EmailTemplates", "BaseTemplate.html");
        return File.ReadAllText(path);
    }
    
    public static string LoginTemplate(string email)
    {
        var template = OtenerPlantillaHtml();
        template = template.Replace("{{Titulo}}", "Inicio de sesion");
        template = template.Replace("{{Descripcion}}", "Le informamos que se ha iniciado sesion en su cuenta");
        template = template.Replace("{{Fecha}}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        return template;
    }
    
    public static string RegisterTemplate(string email)
    {
        var template = OtenerPlantillaHtml();
        template = template.Replace("{{Titulo}}", "Registro de usuario");
        template = template.Replace("{{Descripcion}}", "Le informamos que se ha registrado un usuario en su cuenta");
        template = template.Replace("{{Fecha}}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        return template;
    }
    
    public static string ResetPasswordTemplate(string email, string token)
    {
        var template = OtenerPlantillaHtml();
        template = template.Replace("{{Titulo}}", "Recuperacion de contraseña");
        template = template.Replace("{{Descripcion}}", "Le informamos que se ha solicitado la recuperacion de contraseña");
        template = template.Replace("{{Fecha}}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        template = template.Replace("{{Enlace}}", $"https://localhost:5001/api/auth/resetpassword?email={email}&token={token}");
        return template;
    }
    
    public static string ChangePasswordTemplate(string email)
    {
        var template = OtenerPlantillaHtml();
        template = template.Replace("{{Titulo}}", "Cambio de contraseña");
        template = template.Replace("{{Descripcion}}", "Le informamos que se ha cambiado la contraseña de su cuenta");
        template = template.Replace("{{Fecha}}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        return template;
    }
}