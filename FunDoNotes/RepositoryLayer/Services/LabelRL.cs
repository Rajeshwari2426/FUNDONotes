using CommonLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class LabelRL : ILabelRL
    {
        private readonly FunDoContext funDoContext;
        public LabelRL(FunDoContext funDoContext)
        {
            this.funDoContext = funDoContext;
        }
        public LabelEntity AddLabel(Label label, long userId)
        {
            try
            {
                var duplicate = funDoContext.LabelTable.Where(x => x.LabelName == label.LabelName && x.NoteId == label.NoteId && x.UserId == userId).FirstOrDefault();
                var resNote = funDoContext.NotesTable.Where(x => x.NoteId == label.NoteId && x.UserId == userId).FirstOrDefault();

                if (duplicate == null && resNote != null)
                {
                    LabelEntity labelEntity = new LabelEntity()
                    {
                        LabelName = label.LabelName,
                        NoteId = label.NoteId,
                        UserId = userId
                    };
                    funDoContext.LabelTable.Add(labelEntity);
                    int res = funDoContext.SaveChanges();
                    if (res > 0)
                        return labelEntity;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public LabelEntity EditLabel(string newName, long labelId, long userId)
        {
            try
            {
                var result = funDoContext.LabelTable.Where(x => x.LabelId == labelId && x.UserId == userId).FirstOrDefault();

                if (result != null)
                {
                    result.LabelName = newName;

                    funDoContext.LabelTable.Update(result);
                    int res = funDoContext.SaveChanges();
                    if (res > 0)
                        return result;
                    else
                        return null;
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string RemoveLabel(long labelId, long noteId, long userId)
        {
            try
            {
                var resLabel = funDoContext.LabelTable.Where(x => x.LabelId == labelId && x.NoteId == noteId && x.UserId == userId).FirstOrDefault();

                funDoContext.LabelTable.Remove(resLabel);
                int res = funDoContext.SaveChanges();
                if (res > 0)
                    return "Label removed successfully";
                else
                    return "Failed to remove the label";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<LabelEntity> GetAll()
        {
            try
            {
                var getLabels = funDoContext.LabelTable.ToList();
                return getLabels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
