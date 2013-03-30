
using System.Windows.Forms;
namespace CogEngine.Objects.WinForms
{
    public interface ICogEngineWinControl
    {
        ConcentradorObjeto Objeto { get; }
        Control InitWinControl();
        Control GetControl();
        string IDScript { get; set; }
        int ZIndex { get; set; }
    }
}
