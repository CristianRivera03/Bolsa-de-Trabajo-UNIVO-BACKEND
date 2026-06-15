using System;

namespace PortalTrabajo.Utility
{
    public static class EmailTemplateBuilder
    {
        public static string BuildTemplate(string title, string content)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>{title}</title>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f7f6;
            margin: 0;
            padding: 0;
            color: #333333;
        }}
        .container {{
            max-width: 600px;
            margin: 40px auto;
            background: #ffffff;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 4px 15px rgba(0,0,0,0.05);
        }}
        .header {{
            background: linear-gradient(135deg, #0d47a1 0%, #1976d2 100%);
            color: #ffffff;
            padding: 30px;
            text-align: center;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
            font-weight: 600;
        }}
        .content {{
            padding: 30px;
            line-height: 1.6;
            font-size: 16px;
        }}
        .footer {{
            background-color: #f8f9fa;
            color: #777777;
            text-align: center;
            padding: 20px;
            font-size: 13px;
            border-top: 1px solid #eeeeee;
        }}
        .btn {{
            display: inline-block;
            padding: 12px 24px;
            margin-top: 20px;
            background-color: #1976d2;
            color: #ffffff !important;
            text-decoration: none;
            border-radius: 4px;
            font-weight: bold;
        }}
        .alert-box {{
            background-color: #fff3cd;
            color: #856404;
            padding: 15px;
            border-left: 4px solid #ffeeba;
            border-radius: 4px;
            margin: 15px 0;
        }}
        ul {{
            padding-left: 20px;
            background: #f8f9fa;
            padding: 15px 15px 15px 35px;
            border-radius: 5px;
        }}
        li {{
            margin-bottom: 8px;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h1>{title}</h1>
        </div>
        <div class=""content"">
            {content}
        </div>
        <div class=""footer"">
            <p><strong>Bolsa de Trabajo UNIVO</strong></p>
            <p>Este es un correo generado automáticamente. Por favor, no respondas a esta dirección.</p>
        </div>
    </div>
</body>
</html>";
        }
    }
}
