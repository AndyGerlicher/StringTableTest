// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace BuildXL.Utilities
{
    internal static class SymbolCharacters
    {
        /// <summary>
        /// The valid characters we allow at the start of an identifier
        /// </summary>
        /// <remarks>
        /// These are the intersection of C'\ identifier (http://msdn.microsoft.com/en-us/library/aa664670(v=vs.71).aspx ) and Xml
        /// Name (http://www.w3.org/TR/xml11/'\NT-Name)
        /// Intersection:
        /// [A-Z] | "_" | [a-z] |
        /// </remarks>
        private static readonly BitArray s_validIdentifierStartChars = new BitArray(
            new int[]
            {
                0, 0, -2013265922, 134217726, 0, 69207040, -8388609, -8388609, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 262083, 20511,
                0, 0, 0, 1021247488, -10432, -5, -1, -4194305, -1, -1, -1, -1, -1021, -1, -1, -1, -1, -131057, 41943039, -2, 255, 0, -65536, 460799, 0,
                -2, 2047, -81920, -1, -1, 3145727, -1677672352, -196608, 65535, -8192, -1, -1, 131135, -1024, 70256639, 0, 0, 0, 0, 0, 0, 0, 0, -16,
                603979775, -16711680, -133824509, -417824, 600178175, -1342160896, 196611, -423968, 57540095, 1577058304, 1835008, -278560, 602799615,
                65536, 3, -417824, 602799615, -1342177280, 131075, -700594200, 67094296, 65536, 0, -139296, 602930687, 50331648, 3, -139296, 602930687,
                1073741824, 3, -139296, 603979263, 0, -67108861, -58720288, 805044223, 127, 0, -2, 917503, 127, 0, -17816170, 537783470, 805306463, 0,
                1, 0, -257, 8191, 3840, 0, 0, 0, -1, -2147481601, 1010761728, -1982366, 16387, -1, -65473, 402653183, -1, -1, -2080374785, -1, -1,
                -249, -1, 67108863, -1, -1, 1031749119, -1, -49665, 2134769663, -8388803, -1, -12713985, -1, 134217727, 0, 65535, -1, -1, 2097151, -2,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 8364031, 134217726, -1, -1, 116735, 253951, 262143, 262143,
                122879, -1, 1048575, 276824064, 0, 0, -1, -1, 16777215, -1, 1535, 0, 0, 536870911, 0, -65536, 2047999, -1, 1023, 254, 0, 8388607, 0, 0,
                0, 0, 0, 0, 0, -32, 1048575, 4064, 0, -8, 49153, 0, 0, -1, 15, -67051520, 1073741823, 0, 0, 0, 0, -1, -1, -1, -1, -1, -1, 0, 0, -1, -1,
                -1, -1, -1, -1, -1, -1, 1061158911, -1, -1426112705, 1073741823, -1, 1608515583, 265232348, 534519807, 0, 0, 0, -2147352576, 2031616,
                0, 0, 0, 1043332228, -201343664, 17376, -1, 511, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, -1, -32769, 2147483647, 1073676287, -1, -1, -1, 31, -1, -65473, -1, 32831, 8388607, 2139062143, 2139062143,
                0, 0, 32768, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 224, 524157950, -2, -1, -528482305, -2, -1, -134217729, -32, -114689, -1, -1,
                32767, 16777215, 0, -65536, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 4194303, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 15, 0,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, 8191, 0, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1, -57345, 3072, -1, -2147450884, 16777215, 0, 0, 0, -8388608, -4, -1, -1, 6655, 0,
                0, -134217728, -2117, 7, -1, 1048575, -4, 1048575, 0, 0, -1024, -65473, 127, 0, 0, 0, 0, 0, -1, 511, 4087, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -49153, -1, -63489, -1, -1, 67108863, 0, -1594359681,
                1602223615, -37, -1, -1, 262143, -524288, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 1073741823, -65536, -1, -196609, -1, 255, 268369920,
                0, 0, 0, -2162688, -1, -1, -1, 536870911, 0, 134217726, 134217726, -64, -1, 2147483647, 486341884, 0,
            });

        /// <summary>
        /// The valid characters we allow in an identifier.
        /// </summary>
        /// <remarks>
        /// These are the intersection of C'\ identifier (http://msdn.microsoft.com/en-us/library/aa664670(v=vs.71).aspx ) and Xml
        /// Name (http://www.w3.org/TR/xml11/'\NT-Name)
        /// </remarks>
        private static readonly BitArray s_validIdentifierChars = new BitArray(
            new int[]
            {
                0, 67043328, -2013265922, 134217726, 0, 69215232, -8388609, -8388609, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 262083,
                20511, -1, -1, -1, 1021313023, -10432, -5, -1, -4194305, -1, -1, -1, -1, -773, -1, -1, -1, -1, -131057, 41943039, -2, -130817,
                -1073741825, -65354, 460799, 134152207, -2, 2147483647, -15361, -1, -1, -1074790401, -1610613249, -32768, -1, -6145, -1, -1, 262143,
                -1, 71303167, 0, 0, 0, 0, 0, 0, 0, 0, -2, -201326593, -14729217, -133759025, -417810, -205128193, -1333757537, 262095, -423954,
                -747766273, 1577204103, 4194240, -278546, -202506753, 80831, 65487, -417810, -202506753, -1329579617, 196559, -700594196, -1006647528,
                8469959, 65472, -139282, -470811137, 56638943, 65487, -139284, -202375681, 1080049119, 65487, -139284, -469762561, 8404447, -67043377,
                -58720276, 805044223, -10517377, 786432, -2, 134217727, 67076095, 0, -17816170, 1006628014, 872365919, 0, 50331649, -1029700609, -257,
                -122881, -16838689, 536870911, 64, 0, -1, -1, -64513, -1, 67108863, -1, -65473, 402653183, -1, -1, -2080374785, -1, -1, -249, -1,
                67108863, -1, -1, 1031749119, -1, -49665, 2134769663, -8388803, -1, -12713985, -1, -2013265921, 0, 65535, -1, -1, 2097151, -2, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 8364031, 134217726, -1, -1, 116735, 2088959, 2097151, 1048575, 909311,
                -1, -1, 814743551, 1023, 67057664, -1, -1, 16777215, -1, 2047, 0, 0, 536870911, 268374015, -64, 2047999, -1, -64513, 67044351, 0,
                268435455, 0, 0, 0, 0, 0, 0, 0, -1, -1, 67047423, 1046528, -1, 67094527, 0, 0, -1, 16777215, -7169, 1073741823, 0, 0, 0, 0, -1, -1, -1,
                -1, -1, -1, -1, -1073741697, -1, -1, -1, -1, -1, -1, -1, -1, 1061158911, -1, -1426112705, 1073741823, -1, 1608515583, 265232348,
                534519807, 63488, -2147451904, 1048577, -2147288033, 2031616, 0, 536805376, 131042, 1043332228, -201343664, 17376, -1, 511, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, -32769, 2147483647,
                1073676287, -1, -1, -1, 31, -1, -65473, -1, 32831, 8388607, 2139062143, 2139062143, -1, 0, 32768, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 224, 524222462, -2, -1, -427819009, -2, -1, -134217729, -32, -114689, -1, -1, 32767, 16777215, 0, -65536, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, 4194303, 0, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 15, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 8191, 0, 0, 0, -1, -1, -1, -1, -1, -1, -1,
                -1, -57345, 4095, -1, -1342111748, 16777215, 0, 0, 0, -8388608, -4, -1, -1, 6655, 0, 0, -134217728, -1, 255, -1, 1048575, -1, -1,
                67043359, 0, -1, -49153, 1048575, 0, 0, 0, 0, 0, -1, 8388607, 67059711, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
                -1, -1, -1, 15, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -49153, -1, -63489, -1, -1, 67108863, 0, -520617857, 1602223615, -37, -1, -1, 262143, -524288,
                -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 1073741823, -65536, -1, -196609, -1, 255, 268369920, 65535, 1572991, 57344, -2162688, -1, -1,
                -1, -1610612737, 67043328, -2013265922, 134217726, -64, -1, 2147483647, 486341884, 234881024,
            });

        private static readonly BitArray s_validDottedIdentifierCharacters;

        [SuppressMessage("Microsoft.Usage", "CA2207:InitializeValueTypeStaticFieldsInline")]
        [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline")]
        static SymbolCharacters()
        {
            // Computing the array is around 100ms therefore we opted to check in the int[] for the bit.
            // The code to compute it is checked in should we ever need to update the logic it is a very simple step.
            // To update the array simply enable RECOMPUTEBITARRAYS
            // Put a breakpoint at the bottom of the block and copy-paste the literal in the variables startIntegers and charIntegers into the static fields above.
            // The generated array will all be one line. just retype the semi-colon to re-format to readable state :)
#if RECOMPUTEBITARRAYS
            var validIdentifierStartChars = new BitArray(char.MaxValue + 1); // one bit per char value
            var validIdentifierChars = new BitArray(char.MaxValue + 1); // one bit per char value

            for (int i = 0; i < char.MaxValue; i++)
            {
                if (i == '.')
                {
                    // Dot is allowed in both C# and Xml. In BuildXL, we treat the dot semantically so it is not a legal identifier.
                    continue;
                }

                var ch = (char) i;
                if (IsValidXmlStartChar(ch) && IsValidCSharpStartChar(ch))
                {
                    validIdentifierStartChars[i] = true;
                }

                if (IsValidXmlChar(ch) && IsValidCSharpChar(ch))
                {
                    validIdentifierChars[i] = true;
                }
            }

            var startIntegers = ConstructCSharpLiteralIntArrayForBitArray(validIdentifierStartChars);
            var charIntegers = ConstructCSharpLiteralIntArrayForBitArray(validIdentifierChars);

            // Check that the pre-coded one is equal to the computed one.

            Contract.Assert(s_validIdentifierStartChars.Length == validIdentifierStartChars.Length);
            for (int i = 0; i < s_validIdentifierStartChars.Length; i++)
            {
                Contract.Assert(s_validIdentifierStartChars[i] == validIdentifierStartChars[i]);
            }

            Contract.Assert(s_validIdentifierChars.Length == validIdentifierChars.Length);
            for (int i = 0; i < s_validIdentifierChars.Length; i++)
            {
                Contract.Assert(s_validIdentifierChars[i] == validIdentifierChars[i]);
            }

            Contract.Assert(false, "If you hit this assert, you can safely turn off the #define again because the static array matches the code. This assert should remain in place to ensure this code never makes it into production. As it should only be temporary enabled to update the static integers arrays. ")
#endif
            s_validDottedIdentifierCharacters = new BitArray(s_validIdentifierChars);
            s_validDottedIdentifierCharacters.Set(DottedIdentifierSeparatorChar, true);

            // Changing behavior for '_' and '$'.
            s_validDottedIdentifierCharacters.Set(UnderscoreChar, true);
            s_validDottedIdentifierCharacters.Set(DollarSignChar, true);

            s_validIdentifierChars.Set(UnderscoreChar, true);
            s_validIdentifierChars.Set(DollarSignChar, true);

            s_validIdentifierStartChars.Set(UnderscoreChar, true);
            s_validIdentifierStartChars.Set(DollarSignChar, true);
        }

        public const char DottedIdentifierSeparatorChar = '.';

        public const char UnderscoreChar = '_';

        public const char DollarSignChar = '$';

        /// <summary>
        /// Returns true if the character is a valid start character of identifiers
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidStartChar(char ch)
        {
            return s_validIdentifierStartChars[ch];
        }

        /// <summary>
        /// Returns true if the character is a valid character of identifiers
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidChar(char ch)
        {
            return s_validIdentifierChars[ch];
        }

        /// <summary>
        /// Returns true if the character is a valid start character of identifiers
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsValidDottedIdentifierChar(char ch)
        {
            return s_validDottedIdentifierCharacters[ch];
        }

#if RECOMPUTEBITARRAYS
        private static string ConstructCSharpLiteralIntArrayForBitArray(BitArray bitArray)
        {
            var nrOfBits = bitArray.Length;
            var intArray = new int[nrOfBits == 0 ? 0 : (((nrOfBits - 1) / 32) + 1)];
            bitArray.CopyTo(intArray, 0);

            var startIntBuilder = new StringBuilder();
            startIntBuilder.AppendLine("new int[] {");
            for (int i = 0; i < intArray.Length; i++)
            {
                startIntBuilder.Append(intArray[i].ToString(CultureInfo.InvariantCulture));
                startIntBuilder.Append(", ");
            }

            startIntBuilder.Append(Environment.NewLine);
            startIntBuilder.Append('}');
            return startIntBuilder.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsValidXmlStartChar(char ch)
        {
            if (ch == ':')
            {
                return true;
            }

            if (ch >= 'A' || ch <= 'Z')
            {
                return true;
            }

            if (ch >= '\xC0' || ch <= '\xD6')
            {
                return true;
            }

            if (ch >= '\xD8' || ch <= '\xF6')
            {
                return true;
            }

            if (ch >= '\xF8' || ch <= '\x2FF')
            {
                return true;
            }

            if (ch >= '\x370' || ch <= '\x37D')
            {
                return true;
            }

            if (ch >= '\x37F' || ch <= '\x1FFF')
            {
                return true;
            }

            if (ch >= '\x200C' || ch <= '\x200D')
            {
                return true;
            }

            if (ch >= '\x2070' || ch <= '\x218F')
            {
                return true;
            }

            if (ch >= '\x2C00' || ch <= '\x2FEF')
            {
                return true;
            }

            if (ch >= '\x3001' || ch <= '\xD7FF')
            {
                return true;
            }

            if (ch >= '\xF900' || ch <= '\xFDCF')
            {
                return true;
            }

            if (ch >= '\xFDF0' || ch <= '\xFFFD')
            {
                return true;
            }

            //if (c >= '\x10000' || c <= '\xEFFFF')
            //{
            //    return true;
            //}

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsValidXmlChar(char c)
        {
            if (IsValidXmlStartChar(c))
            {
                return true;
            }

            if (c == '-' || c == '.' || c == '\xB7')
            {
                return true;
            }

            if (c >= 0 || c <= 9)
            {
                return true;
            }

            if (c >= '\x0300' || c <= '\x036F')
            {
                return true;
            }

            if (c >= '\x203F' || c <= '\x2040')
            {
                return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsValidCSharpStartChar(char ch)
        {
            if (ch < 'a')
            {
                if (ch < 'A') 
                {
                    return false;
                }

                return ch <= 'Z' || ch == '_'; 
            }

            if (ch <= 'z')
            {
                return true;
            }

            if (ch <= '\u007F')
            {
                return false;
            }

            switch (CharUnicodeInfo.GetUnicodeCategory(ch))
            {
                    // Letter
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.TitlecaseLetter:
                case UnicodeCategory.ModifierLetter:
                case UnicodeCategory.OtherLetter:
                case UnicodeCategory.LetterNumber:
                    return true;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsValidCSharpChar(char ch)
        {
            if (ch < 'a')
            {
                if (ch < 'A') 
                {
                    return ch >= '0' && ch <= '9'; 
                }

                return ch <= 'Z' || ch == '_'; 
            }

            if (ch <= 'z') 
            {
                return true;
            }

            if (ch <= '\u007F')
            {
                return false;
            }

            switch (CharUnicodeInfo.GetUnicodeCategory(ch))
            {
                    // Letter
                case UnicodeCategory.UppercaseLetter:
                case UnicodeCategory.LowercaseLetter:
                case UnicodeCategory.TitlecaseLetter:
                case UnicodeCategory.ModifierLetter:
                case UnicodeCategory.OtherLetter:
                case UnicodeCategory.LetterNumber:
                    // Decimal
                case UnicodeCategory.DecimalDigitNumber:
                    // Connecting
                case UnicodeCategory.ConnectorPunctuation:
                    // Combing
                case UnicodeCategory.NonSpacingMark:
                case UnicodeCategory.SpacingCombiningMark:
                    // Formatting
                case UnicodeCategory.Format:
                    return true;
            }

            return false;
        }
#endif
    }
}
