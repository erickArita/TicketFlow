namespace TicketFlow.Helpers;

public class EmailTemplates
{
    private static string OtenerPlantillaHtml()
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "BaseTemplate.html");
        var archivo = File.ReadAllText(path);
        return archivo;
    }
    
    public static string LoginTemplate()
    {
        var titulo = "Bienvenido a TicketFlow";
        var descripcion = "Bienvenido a TicketFlow";
        return CreateTemplate(titulo, descripcion);
    }
    
    public static string RegisterTemplate(string titulo, string descripcion)
    {
        return CreateTemplate(titulo, descripcion);
    }
    
    public static string ResetPasswordTemplate(string titulo, string descripcion)
    {
        return CreateTemplate(titulo, descripcion);
    }
    
    public static string ChangePasswordTemplate(string titulo, string descripcion)
    {
        return CreateTemplate(titulo, descripcion);
    }

    private static string CreateTemplate(string titulo, string descripcion)
    {
        var template = OtenerPlantillaHtml();
        template = template.Replace("{{Titulo}}", titulo);
        template = template.Replace("{{Descripcion}}", descripcion);
        template = template.Replace("{{Fecha}}", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
        return template;
    }
}