using System.Drawing;

namespace CogEngine.Objects
{
    public interface ITexto
    {
        int PosicaoX { get; set; }
        int PosicaoY { get; set; }
        string Texto { get; set; }
        float TamanhoFonte { get; set; }
        Color Cor { get; set; }
        int ZIndex { get; set; }
    }
}
