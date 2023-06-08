////DESKTOP - 4098PCL\gwred
//2023 - 01 - 09 15:02:01

namespace MusicJammer.Data
{
    /// <summary>
    /// Defines the <see cref="Style" />.
    /// It is used premiered to get static colours for views.
    /// </summary>
    public static class Style
    {
        /// <summary>
        /// Defines the color names.
        /// </summary>
        public enum ColorName
        {
            /// <summary>
            /// Defines the color: Davys grey.
            /// </summary>
            DavysGrey,

            /// <summary>
            /// Defines the color: Barbie pink.
            /// </summary>
            BarbiePink,

            /// <summary>
            /// Defines the color: French rose.
            /// </summary>
            FrenchRose,

            /// <summary>
            /// Defines the color: Arylide yellow.
            /// </summary>
            ArylideYellow,

            /// <summary>
            /// Defines the color: White.
            /// </summary>
            White
        }

        /// <summary>
        /// Defines the level.
        /// </summary>
        public enum Level : int
        {
            /// <summary>
            /// Defines the one hundred.
            /// </summary>
            one_hundred = 8,

            /// <summary>
            /// Defines the eighty seven and five tenths.
            /// </summary>
            eighty_seven_and_five_tenths = 7,

            /// <summary>
            /// Defines the seventy five.
            /// </summary>
            seventy_five = 6,

            /// <summary>
            /// Defines the sixty two and five tenths.
            /// </summary>
            sixty_two_and_five_tenths = 5,

            /// <summary>
            /// Defines the fifty.
            /// </summary>
            fifty = 4,

            /// <summary>
            /// Defines the thirty seven and five tenths.
            /// </summary>
            thirty_seven_and_five_tenths = 3,

            /// <summary>
            /// Defines the twenty five.
            /// </summary>
            twenty_five = 2,

            /// <summary>
            /// Defines the twelve and five tenths.
            /// </summary>
            twelve_and_five_tenths = 1,

            /// <summary>
            /// Defines the defult.
            /// </summary>
            defult = 0,

            /// <summary>
            /// Defines the negative twelve and five tenths.
            /// </summary>
            negative_twelve_and_five_tenths = -1,

            /// <summary>
            /// Defines the negative twenty five.
            /// </summary>
            negative_twenty_five = -2,

            /// <summary>
            /// Defines the negative thirty seven and five tenths.
            /// </summary>
            negative_thirty_seven_and_five_tenths = -3,

            /// <summary>
            /// Defines the negative fifty.
            /// </summary>
            negative_fifty = -4,

            /// <summary>
            /// Defines the negative sixty two and five tenths.
            /// </summary>
            negative_sixty_two_and_five_tenths = -5,

            /// <summary>
            /// Defines the negative_seventy_five.
            /// </summary>
            negative_seventy_five = -6,

            /// <summary>
            /// Defines the negative eighty seven and five tenths.
            /// </summary>
            negative_eighty_seven_and_five_tenths = -7,

            /// <summary>
            /// Defines the negative one hundred.
            /// </summary>
            negative_one_hundred = -8,
        }

        /// <summary>
        /// Defines the color: Davys grey.
        /// </summary>
        public static RGB DavysGrey = new RGB("#50514F");

        /// <summary>
        /// Defines the color: Barbie pink.
        /// </summary>
        public static RGB BarbiePink = new RGB("#D72483");

        /// <summary>
        /// Defines the color: French rose.
        /// </summary>
        public static RGB FrenchRose = new RGB("#FD3E81");

        /// <summary>
        /// Defines the color: Arylide yellow.
        /// </summary>
        public static RGB ArylideYellow = new RGB("#E5D352");

        /// <summary>
        /// Defines the color: White.
        /// </summary>
        public static RGB White = new RGB("#FFFFFF");
    }
}
