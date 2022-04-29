using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface INotesRL
    {
        public NotesEntity CreateNote(Notes createNotes, long userID);
        public List<NotesEntity> RetriveNotes(long userID);
        public NotesEntity UpdateNote(Notes createNotes, long noteID, long userID);
        public string DeleteNote(long noteID, long userID);
        public NotesEntity IsArchieveOrNot(long noteID, long userID);
        public NotesEntity IsPinnedOrNot(long noteID, long userID);
        public NotesEntity IsTrashOrNot(long noteID, long userID);
        public NotesEntity UploadImage(long noteID, long userID, IFormFile imagePath);

    }
}
