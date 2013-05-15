
namespace CogEngine.Objects.XNA
{
    public class CustomImageXNAControl : AbstractImagemXNAControl, ICustomImage
    {
        public CustomImageXNAControl(ImagemObjeto objeto) : base(objeto) { }

        public override string CaminhoImagem
        {
            get;
            set;
        }
    }
}
