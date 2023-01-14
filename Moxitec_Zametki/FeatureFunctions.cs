using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moxitec_Zametki
{
    public static class FeatureFunctions
    {
        /// <summary>
        /// Преобразует символ в булеановское значение
        /// </summary>
        /// <returns>True if 1, False in etc..</returns>
        public static bool CharToBool(char value)
        {
            if (value == '1'){ return true; } else {return false;}
        }
        
        public static Note[] NoteContentElementToArray(ObservableCollection<NoteContentElement> note_content)
        {
            Note[] notes = new Note[note_content.Count()];
            for (int i = 0; i < note_content.Count(); i++)
            {
                notes[i] = note_content[i].getNote();
            }
            return notes;
        }
    }
}
