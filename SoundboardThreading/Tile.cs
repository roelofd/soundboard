﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SoundboardThreading
{
    class Tile
    {
        public TextBox textBox { get; set; }
        public TextBlock textBlock { get; set; }
        public Button playButton { get; set; }
        public Button downloadButton { get; set; }

        string fileLocation;    // File location of the corresponding sound

        public int Column { get; set; }
        public int Row { get; set; }

        public Tile(TextBox _textBox, TextBlock _textBlock, Button _playButton, Button _downloadButton, int _column, int _row)
        {
            textBox = _textBox;
            textBlock = _textBlock;
            playButton = _playButton;
            downloadButton = _downloadButton;
            
            playButton.Click += Play_Button;
            downloadButton.Click += Download_Click;

            Column = _column;
            Row = _row;
        }

        private void Download_Click(object sender, RoutedEventArgs e)
        {
            if(textBox.Text == string.Empty)
            {
                return;
            }
            var url = new Uri(textBox.Text);
            var downloader = new YoutubeDownloader();
            fileLocation = downloader.Download(url.ToString());

            if (fileLocation != null)
            {
                downloadButton.Visibility = Visibility.Collapsed;
                textBox.Visibility = Visibility.Collapsed;
                playButton.Visibility = Visibility.Visible;
                textBlock.Text = fileLocation.Split(".")[0];
                textBlock.Visibility = Visibility.Visible;
            }
        }

        /*
         * Click event handler for clicking the play button
         */
        private void Play_Button(object sender, RoutedEventArgs e)
        {
            var audioManager = new AudioManager();
            audioManager.Play(fileLocation);
        }
    }
}
