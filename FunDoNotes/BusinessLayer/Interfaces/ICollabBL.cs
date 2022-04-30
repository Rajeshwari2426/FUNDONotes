using CommonLayer.Models;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface ICollabBL
    {
        public CollabEntity AddCollaborator(Collaborators collaborator, long noteID, long userID);
        public string RemoveCollaborator(long collabID, long noteID);
        public List<CollabEntity> GetAll(long noteID);
    }
}
