using FundooNotesModelLayer.Models;
using FundooNotesRepositoryLayer.IRepository;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace FundooNotesRepositoryLayer.Repository
{
    public class NotesRepository : INotesRepository
    {
        private readonly IConfiguration configuration;
        public NotesRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public object AddNotes(NotesModel notesModel)
        {
            try
            {
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand("sp_AddNotes", _db))
                {
                    cmd.Connection = _db;

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("noteid", notesModel.NoteId);
                    cmd.Parameters.Add("email", notesModel.Email);
                    cmd.Parameters.Add("title", notesModel.Title);
                    cmd.Parameters.Add("description", notesModel.Description);
                    cmd.Parameters.Add("createddate", notesModel.CreatedDate);
                    cmd.Parameters.Add("modifieddate", notesModel.ModifiedDate);
                    cmd.Parameters.Add("image", notesModel.Image);
                    cmd.Parameters.Add("reminder", notesModel.Reminder);
                    cmd.Parameters.Add("archive", notesModel.IsArchive);
                    cmd.Parameters.Add("trash", notesModel.IsTrash);
                    cmd.Parameters.Add("pin", notesModel.IsPin);
                    cmd.Parameters.Add("color", notesModel.Color);
                    cmd.Parameters.Add("senderemail", notesModel.SenderEmail);
                    cmd.Parameters.Add("receiveremail", notesModel.ReceiverEmail);
                    cmd.Parameters.Add("label", notesModel.Label);

                    _db.Open();
                    int value = cmd.ExecuteNonQuery();
                    _db.Close();
                    if (value != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool Archive(int NoteId)
        {
            string sqlQuery = "SELECT * FROM Notes_Details WHERE IsArchive=0 and NoteId =" + NoteId;

            using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
            using (OracleCommand cmd = new OracleCommand(sqlQuery, _db))
            {

                cmd.Connection = _db;
                _db.Open();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        MakeArchive(NoteId);
                        _db.Close();
                        return true;
                    }
                    else
                    {
                        MakeUnArchive(NoteId);
                        _db.Close();
                        return true;
                    }
                }
            }

        }
        private void MakeArchive(int NoteId)
        {
            using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
            using (OracleCommand cmd = new OracleCommand("sp_MakeArchive", _db))
            {
                cmd.Connection = _db;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("noteid", NoteId);
                _db.Open();

                int value = cmd.ExecuteNonQuery();
                _db.Close();

            }
        }
        private void MakeUnArchive(int NoteId)
        {
            using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
            using (OracleCommand cmd = new OracleCommand("sp_MakeUnArchive", _db))
            {
                cmd.Connection = _db;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("noteid", NoteId);
                _db.Open();

                int value = cmd.ExecuteNonQuery();
                _db.Close();

            }
        }

        public bool TrashAndUnTrash(int NoteId)
        {
            string sqlQuery = "SELECT * FROM Notes_Details WHERE IsTrash=0 and NoteId =" + NoteId;

            using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
            using (OracleCommand cmd = new OracleCommand(sqlQuery, _db))
            {

                cmd.Connection = _db;
                _db.Open();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    if (sdr.HasRows)
                    {
                        MakeTrash(NoteId);
                        _db.Close();
                        return true;
                    }
                    else
                    {
                        MakeUnTrash(NoteId);
                        _db.Close();
                        return true;
                    }
                }
            }

        }
        private void MakeTrash(int NoteId)
        {
            using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
            using (OracleCommand cmd = new OracleCommand("sp_MakeTrash", _db))
            {
                cmd.Connection = _db;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("noteid", NoteId);
                _db.Open();

                int value = cmd.ExecuteNonQuery();
                _db.Close();

            }
        }
        private void MakeUnTrash(int NoteId)
        {
            using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
            using (OracleCommand cmd = new OracleCommand("sp_MakeUnTrash", _db))
            {
                cmd.Connection = _db;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add("noteid", NoteId);
                _db.Open();

                int value = cmd.ExecuteNonQuery();
                _db.Close();

            }
        }

        public bool DeleteNote(int noteId)
        {
            try
            {

                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = _db;
                    cmd.CommandText = "sp_DeleteNote";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("NoteId", noteId);
                    _db.Open();
                    int value = cmd.ExecuteNonQuery();
                    _db.Close();
                    if (value >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool UpdateNote(int noteid, string title, string description)
        {
            if (description != null && title != null)
            {
                try
                {
                    using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = _db;
                        cmd.CommandText = "sp_UpdateNote";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("noteid", noteid);
                        cmd.Parameters.Add("title", title);
                        cmd.Parameters.Add("description", description);
                        _db.Open();
                        int value = cmd.ExecuteNonQuery();
                        _db.Close();
                        if (value >= 1)
                        {
                            return true;
                        }
                        else
                        {
                            return false;

                        }
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return false;
        }
    }
}
