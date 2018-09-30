using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace SmartQueue.Utils
{
    public static class Aplicacao
    {
        private const string Link = "http://smartqueueapi.azurewebsites.net/api/";

        public static string Url(string route, string metodo)
        {
            return string.Concat(Link, string.Format("{0}/{1}", route, metodo));
        }

        public static string Url(string route, string metodo, string parametro)
        {
            return string.Concat(Link, string.Format("{0}/{1}/{2}", route, metodo, parametro));
        }

        public static StringContent Content(string json)
        {
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public static bool EmailValido(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            Regex expressaoRegex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");

            if (expressaoRegex.IsMatch(email))
                return true;
            else
                return false;
        }

        public static bool CpfValido(string cpf)
        {
            string primeirosDigtos;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            if (cpf.Length != 11) return false;

            primeirosDigtos = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 10; i >= 2; i--)
            {
                int x = 10 - i;
                soma += int.Parse(primeirosDigtos[x].ToString()) * i;
            }

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            primeirosDigtos = primeirosDigtos + digito;

            soma = 0;

            for (int i = 11; i >= 2; i--)
            {
                int x = 11 - i;
                soma += int.Parse(primeirosDigtos[x].ToString()) * i;
            }
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            if (cpf.EndsWith(digito))
                return true;
            else
                return false;
        }

        public static ImageSource ConverteImagem(byte[] byteArray)
        {
            Image image = new Image();
            image.Source = ImageSource.FromStream(() => new MemoryStream(byteArray));

            return image.Source;
        }

        public static void MostrarLabel(bool mostrar, Entry entry)
        {
            StackLayout layout = (StackLayout)entry.Parent;
            string campo = entry.Placeholder;

            for (int i = 0; i < layout.Children.Count; i++)
            {
                if (layout.Children[i].GetType() == typeof(Label))
                    if (((Label)layout.Children[i]).Text == campo)
                        ((Label)layout.Children[i]).IsVisible = mostrar;
            }
        }

        public static void DesabilitarCampos(bool desabilitar, StackLayout layout)
        {
            for (int i = 0; i < layout.Children.Count; i++)
            {
                if (layout.Children[i].GetType() == typeof(Entry) || layout.Children[i].GetType() == typeof(Button))
                    layout.Children[i].IsEnabled = desabilitar;
            }
        }
    }
}
