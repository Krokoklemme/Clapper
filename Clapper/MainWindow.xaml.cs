//           DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
//                    Version 2, December 2004 
//
// Copyright(C) 2004 Sam Hocevar<sam@hocevar.net>
//
// Everyone is permitted to copy and distribute verbatim or modified
// copies of this license document, and changing it is allowed as long
// as the name is changed.
//
//           DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
//  TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION
//
//  0. You just DO WHAT THE FUCK YOU WANT TO.

namespace Clapper
{
    using System;
    using System.Collections.Generic;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // just a little convenience function
        private static DependencyProperty Reg<T>(string name, T value) => DependencyProperty.Register(name, typeof(T), typeof(MainWindow), new PropertyMetadata(value));

        /// <summary>
        /// Hook to automatically update the output text
        /// </summary>
        /// <param name="e">Argument for the event</param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            // update the result text when any property has changed
            // this saves me from having to write 4 event handlers
            if (e.Property == AllCapitalProperty ||
                e.Property == SelectedIndexProperty ||
                e.Property == InputTextProperty)
            {
                var bfr = InputText.Trim().Replace(" ", ClapVariants[SelectedIndex].Value);

                if (AllCapital)
                {
                    bfr = bfr.ToUpperInvariant();
                }

                OutputText = bfr;
            }
        }

        /// <summary>
        /// Constructs a new <see cref="MainWindow"/>
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Represents a variation of the Clap emoji
        /// </summary>
        public struct ClapType
        {
            /// <summary>
            /// The human-readable name of the emoji
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// The emoji itself
            /// </summary>
            public string Value { get; }

            /// <summary>
            /// Text to be used for user-display
            /// </summary>
            public string DisplayText => $"{Value} {Name}";

            /// <summary>
            /// Defines a new Clap emoji-type
            /// </summary>
            /// <param name="name">The name of the emoji</param>
            /// <param name="value">The actual emoji</param>
            public ClapType(string name, string value)
            {
                Name = name ?? throw new ArgumentNullException(nameof(name));
                Value = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        /// <summary>
        /// Read-only list of all clap-emoji-variants
        /// </summary>
        public static IReadOnlyList<ClapType> ClapVariants { get; } = new[]
        {
            // They might look all the same here and (for some reason)
            // in the final executable, but they're actuall all different
            new ClapType(name: "Normal",        value: "👏"),
            new ClapType(name: "Light",         value: "👏🏻"),
            new ClapType(name: "Medium-Light",  value: "👏🏼"),
            new ClapType(name: "Medium",        value: "👏🏽"),
            new ClapType(name: "Medium-Dark",   value: "👏🏾"),
            new ClapType(name: "Dark",          value: "👏🏿"),
        };

        /// <summary>
        /// Determines whether or not the output text should be all in capital letters
        /// </summary>
        public bool AllCapital
        {
            get { return (bool)GetValue(AllCapitalProperty); }
            set { SetValue(AllCapitalProperty, value); }
        }

        /// <summary>
        /// Backing-store for <see cref="AllCapital"/>
        /// </summary>
        public static readonly DependencyProperty AllCapitalProperty = Reg(nameof(AllCapital), false);

        /// <summary>
        /// The currently selected index for <see cref="ClapVariants"/>
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        /// <summary>
        /// Backing-store for <see cref="SelectedIndex"/>
        /// </summary>
        public static readonly DependencyProperty SelectedIndexProperty = Reg(nameof(SelectedIndex), 0);

        /// <summary>
        /// The text to "clapify"
        /// </summary>
        public string InputText
        {
            get { return (string)GetValue(InputTextProperty); }
            set { SetValue(InputTextProperty, value); }
        }

        /// <summary>
        /// Backing-store for <see cref="InputText"/>
        /// </summary>
        public static readonly DependencyProperty InputTextProperty = Reg(nameof(InputText), string.Empty);

        /// <summary>
        /// "Clapified" text
        /// </summary>
        public string OutputText
        {
            get { return (string)GetValue(OutputTextProperty); }
            set { SetValue(OutputTextProperty, value); }
        }

        /// <summary>
        /// Backing-store for <see cref="OutputText"/>
        /// </summary>
        public static readonly DependencyProperty OutputTextProperty = Reg(nameof(OutputText), string.Empty);
    }
}