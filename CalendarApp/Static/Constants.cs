using CalendarApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarApp.Model
{
	public static class Constants
	{
		public static readonly int DaysOfAWeek = 7;
		public static readonly int FirstElement = 0;
		public static readonly int StartDayNumber = 0;
		public static readonly int NumberOfMonthsInAYear = 12;
		public static readonly int OneDay = 1;
		public static readonly int OneMonth = 1;
		public static readonly int OneYear = 1;
		public static readonly int January = 1;
		public static readonly int December = 12;
        public static readonly List<string> MonthNames = new List<string>(NumberOfMonthsInAYear)
        {
            "Enero",
            "Febrero",
            "Marzo",
            "Abril",
            "Mayo",
            "Junio",
            "Julio",
            "Agosto",
            "Septiembre",
            "Octubre",
            "Noviembre",
            "Diciembre"
        };
        public static readonly int Thursday = 4;
        public static readonly string ColorOfWeekend = "#FF08E8DE";
        public static readonly string ColorOfWeek = "#FFAFEEEE";
        public static readonly string ColorOfToday = "#FF00CED1";
        public static readonly string ColorOfDaysOfOtherMonth = "#FFD7F3F3";
        public static readonly string ColorOfWeekendOfOtherUser = "#FFFA8072";
        public static readonly string ColorOfWeekOfOtherUser = "#FFFFA07A";
        public static readonly string ColorOfTodayOfOtherUser = "#FFD95030";
        public static readonly string ColorOfDaysOfOtherMonthOfOtherUser = "#FFFDE3DB";
        public static readonly string SuccessfulEvent = "¡Evento creado con éxito!";
        public static readonly string SuccessfulEditEvent = "¡Evento editado con éxito!";
        public static readonly string FailedEvent = "Error. Revise que el evento tenga título y que las fechas sean correctas.";
        public static readonly string SuccessfulUser = "¡Usuario creado con éxito!";
        public static readonly string FailedUser = "El nombre de usuario ya existe. Por favor, intente con otro nombre.";
        public static readonly string FailedLogOn = "Nombre de usuario inválido.";
        public static readonly string MainWindow = "MainWindow";
        public static readonly string NotOwnerEditEvent = "¡Ups!...No puedes editar un evento que no es tuyo.";
        public static readonly string NotOwnerDeleteEvent = "¡Ups!...No puedes eliminar este evento.";
        public static readonly string FailedEditEvent = "Error. Revise que el evento tenga título y que las fechas sean correctas.";
        public static readonly string InvalidInvited = "El invitado debe ser válido.";
        public static readonly string InvitedAdded = "Invitado agregado al evento.";
		public static readonly string DeletedEvent = "Este evento ya fue eliminado.";
        public static UserModel CurrentUser 
        { 
            get; 
            set; 
        }
        public static UserModel SelectedUser
        {
            get;
            set;
        }
    }
}
