﻿using MP3Player.Commands;
using MP3Player.Models;
using System.Windows.Input;
using System.Linq;
using System.Windows.Forms;

namespace MP3Player.ViewModels
{
    public class PlaylistViewModel
    {
        private Playlist playlist;
        public Playlist Playlist => playlist;

        public PlaylistViewModel()
        {
            playlist = new Playlist();
            GetSongsPaths = new MainCommand(x => true, x => OpenFileDialog());
            DeleteSongFromPlaylist = new MainCommand(x => CanDeleteOrClear(), x => DeleteFile());
            ClearSongsPaths = new MainCommand(x => CanDeleteOrClear(), x => ClearPlaylist());
        }

        public ICommand GetSongsPaths { get; private set; }
        public ICommand DeleteSongFromPlaylist { get; private set; }
        public ICommand ClearSongsPaths { get; private set; }

        /// <summary>
        /// We're opening a window inside which we can add the songs.
        /// After that we're foreaching over our songs, and get only that
        /// Which end with '.mp3' and add those songs to the playlist
        /// </summary>
        public void OpenFileDialog()
        {
            var fileDialog = new OpenFileDialog { Multiselect = true };
            if (fileDialog.ShowDialog() != null)    
                foreach (var fileName in fileDialog.FileNames.Where(u => System.IO.Path.GetExtension(u).ToLower() == ".mp3"))
                    Playlist.SongsList.Add(fileName);
        }

        //Check if our playlist is not null, then we can remove that.
        public bool CanDeleteOrClear()
        {
            if (Playlist == null)
                return false;
            if (Playlist.SongsList == null)
                return false;
            return true;
        }

        public void DeleteFile()
        {
            Playlist.SongsList.Remove(Playlist.SelectedSong);
        }

        public void ClearPlaylist()
        {
            Playlist.SongsList.ToList().All(y => Playlist.SongsList.Remove(y));
        }
    }
}
