namespace Aafp.Cme.Api.Helpers
{
    public class TemplateHelper<T>
    {
        public string GenerateHtml(T template)
        {
            var type = typeof(T);
            var methodInfo = type.GetMethod("TransformText");
            var templateText = methodInfo.Invoke(template, null);

            return templateText.ToString();
        }
    }
}