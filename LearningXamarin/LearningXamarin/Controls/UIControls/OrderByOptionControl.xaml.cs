using LearningXamarin.Models.Enums;
using Xamarin.Forms;

namespace LearningXamarin.Controls.UIControls
{	
	public partial class OrderByOptionControl : Grid
	{
        public static readonly BindableProperty TitleProperty = BindableProperty
            .Create(
                nameof(Title), //Nombre de la propiedad
                typeof(string), //Que tipo es tu propiedad?
                typeof(OrderByOptionControl)); //Nombre de la clase en la que declaras la BindableProperty

        //Esta es la propiedad a la que le vas a hacer el Binding en el XAML
        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public static readonly BindableProperty IsSelectedProperty = BindableProperty
            .Create(
                nameof(IsSelected),
                typeof(bool),
                typeof(OrderByOptionControl),
                defaultValue: false); //Valor que quieras que tenga tu propiedad por defecto

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly BindableProperty CurrentStateProperty = BindableProperty
            .Create(
                nameof(CurrentState),
                typeof(OrderByEnum),
                typeof(OrderByOptionControl));

        public OrderByEnum CurrentState
        {
            get => (OrderByEnum)GetValue(CurrentStateProperty);
            set => SetValue(CurrentStateProperty, value);
        }

        public static readonly BindableProperty CheckBoxChangedCommandProperty = BindableProperty
            .Create(
                nameof(CheckBoxChangedCommand),
                typeof(Command),
                typeof(OrderByOptionControl));

        public Command CheckBoxChangedCommand
        {
            get => (Command)GetValue(CheckBoxChangedCommandProperty);
            set => SetValue(CheckBoxChangedCommandProperty, value);
        }

        public OrderByOptionControl()
		{
			InitializeComponent();
		}

        public void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (IsSelected)
            {
                CheckBoxChangedCommand?.Execute(CurrentState);
            }
        }
    }
}