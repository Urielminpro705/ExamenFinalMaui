using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TDMPW_411_3P_EX.Models;

namespace TDMPW_411_3P_EX.ViewModels;

public partial class SemestreViewModel : ObservableObject
{
    [ObservableProperty]
    private Semestre _semestre;

    [ObservableProperty]
    private string _nombre;

    [ObservableProperty]
    private string _val1;

    [ObservableProperty]
    private string _val2;

    [ObservableProperty]
    private string _val3;

    [ObservableProperty]
    private string _cal1;

    [ObservableProperty]
    private string _cal2;

    [ObservableProperty]
    private string _mensaje;

    public SemestreViewModel() {
        Semestre = new Semestre {
            Nombre = "Materia",
            ValorParciales = [0,0,0],
            Calificaciones = [0,0],
            CalificacionParaPasar = 0,
            CalificacionParaSerGood = 0
        };
    } 

    [RelayCommand]
    private void ActualizarDatos() {
        Semestre.Nombre = Nombre;
        Semestre.ValorParciales =
        [
            double.TryParse(Val1, out var val1) ? val1 : 0,
            double.TryParse(Val2, out var val2) ? val2 : 0,
            double.TryParse(Val3, out var val3) ? val3 : 0
        ];
        OnPropertyChanged(nameof(Semestre));
    }

    [RelayCommand]
    private void CalcularCalificacion() {
        Semestre.Calificaciones =
        [
            double.TryParse(Cal1, out var cal1) ? cal1 : 0,
            double.TryParse(Cal2, out var cal2) ? cal2 : 0
        ];
        double[] valores = [0,0,0];
        for(int i = 0; i < 3; i++) {
            valores[i] = Semestre.ValorParciales[i] > 0 && Semestre.ValorParciales[i] <= 100 ? Semestre.ValorParciales[i] : 0;
        }
        valores[2] = valores.Sum() == 100 ? valores[2] : (valores.Sum()-valores[2] > 100 ? 0 : 100 - valores.Sum() + valores[2]);
        valores[1] = valores.Sum() == 100 ? valores[1] : 100 - valores.Sum() + valores[1];
        valores[0] = valores.Sum() == 100 ? valores[0] : 100 - valores.Sum() + valores[0];
        Semestre.ValorParciales = valores;
        double[] calificaciones = [0,0];
        for(int i = 0; i < 2; i++) {
            Semestre.Calificaciones[i] = Semestre.Calificaciones[i] > 10 ? 10 : Semestre.Calificaciones[i];
            Semestre.Calificaciones[i] = Semestre.Calificaciones[i] < 0 ? 0 : Semestre.Calificaciones[i];
            calificaciones[i] = Semestre.Calificaciones[i]/10 * Semestre.ValorParciales[i];
        }
        Semestre.CalificacionParaPasar = calificaciones.Sum() < 60 ? Math.Round((60 - calificaciones.Sum())*10/Semestre.ValorParciales[2], 1) : 0;
        Semestre.CalificacionParaSerGood = calificaciones.Sum() < 100 ? Math.Round((100 - calificaciones.Sum())*10/Semestre.ValorParciales[2], 1) : 0;
        if (Semestre.CalificacionParaSerGood > 10 && Semestre.CalificacionParaPasar <= 10) {
            Mensaje = "Ya no alcansaste el 10 pero aun puedes pasar";
        } else {
            if (Semestre.CalificacionParaSerGood > 10 && Semestre.CalificacionParaPasar > 10) {
                Mensaje = "Ve buscando otra carrera";
            } else {
                Mensaje = "Ve por ese 10";
            }
        }
        OnPropertyChanged(nameof(Semestre));
    }
}
