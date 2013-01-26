
namespace CogEngine.Objects
{
    public interface ICustomImage
    {
        int Altura
        {
            get;
            set;
        }

        int Largura
        {
            get;
            set;
        }

        float PosicaoX
        {
            get;
            set;
        }

        float PosicaoY
        {
            get;
            set;
        }

        int ZIndex
        {
            get;
            set;
        }

        string CaminhoImagem { get; set; }
    }
}
