using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using MauiAppBooking.Models;
using System.ComponentModel;

namespace MauiAppBooking.ViewModels
{
    internal class NoteViewModel: ObservableObject, IQueryAttributable
    {
        private Models.Note _note;

        public string Text
        {
            get => _note.Text;
            set 
            {
                if(_note.Text != value)
                {
                    _note.Text = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime Date => _note.Date;
        public string Identifier => _note.FileName;
        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public NoteViewModel()
        {
            _note = new Models.Note();
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        private NoteViewModel(Note note)
        {
            _note = note;
            SaveCommand = new AsyncRelayCommand(Save);
            DeleteCommand = new AsyncRelayCommand(Delete);
        }

        private async Task Save()
        {
            _note.Date = DateTime.Now;
            _note.Save();
            await Shell.Current.GoToAsync($"..?saved={_note.FileName}");
        }


        private async Task Delete()
        {
            _note.Date = DateTime.Now;
            await Shell.Current.GoToAsync($"..?deleted={_note.FileName}");
        }
        void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("load"))
            {
                _note = Note.Load(query["load"].ToString());
                RefreshProperties();
            }
            throw new NotImplementedException();
        }
    }
}
