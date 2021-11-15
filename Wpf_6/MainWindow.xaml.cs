using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Wpf_6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    //1. Разработать в WPF приложении класс WeatherControl, моделирующий погодную сводку –
    //температуру (целое число в диапазоне от -50 до +50),
    //направление ветра (строка),
    //скорость ветра (целое число),
    //наличие осадков (возможные значения: 0 – солнечно, 1 – облачно, 2 – дождь, 3 – снег.
    //Можно использовать целочисленное значение, либо создать перечисление enum).
    //Свойство «температура» преобразовать в свойство зависимости.

    public class WeatherControl : DependencyObject
    {
        public static readonly DependencyProperty TemperatureProperty;
        private string windSide;
        private int windSpeed;
        public enum Percipitation
        {
            sunny = 0,
            cloudy = 1,
            rain = 2,
            snow = 3
        }
        public string WindSide
        {
            get
            {
                return windSide;
            }
            set
            {
                if (value == "north" || value == "south" || value == "west" || value == "east" || value == "southwestern" ||
                     value == "northeastern" || value == "southeastern")
                {
                    windSide = value;
                }
                else
                {
                    windSide = null;
                }
            }
        }
        public int WindSpeed
        {
            get
            {
                return windSpeed;
            }
            set
            {
                if (value > 0 || value <= 60)
                {
                    windSpeed = value;
                }
                else
                {
                    windSpeed = 0;
                }
            }


        }
        public sbyte Temperature
        {
            get => (sbyte)GetValue(TemperatureProperty);
            set => SetValue(TemperatureProperty, value);
        }
        static WeatherControl()
        {
            TemperatureProperty = DependencyProperty.Register(
                nameof(Temperature),
                typeof(sbyte),
                typeof(WeatherControl),
                new FrameworkPropertyMetadata(
                    0,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null,
                   new CoerceValueCallback(CoerceTemperature)),
                new ValidateValueCallback(ValidateTemperature));
        }

        private static bool ValidateTemperature(object value)
        {
            sbyte t = (sbyte)value;
            if (t <= 50)
                return true;
            else
                return false;
        }

        private static object CoerceTemperature(DependencyObject d, object baseValue)
        {
            sbyte t = (sbyte)baseValue;
            if (t >= -50)
                return t;
            else
                return "Ошибка";
        }
    }
}





