// Taken from http://archive.msdn.microsoft.com/SilverlightMD5 LICENSE SEE BELOW (END OF FILE)

//Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Text;

// **************************************************************
// * Raw implementation of the MD5 hash algorithm
// * from RFC 1321.
// *
// * Written By: Reid Borsuk and Jenny Zheng
// * Copyright (c) Microsoft Corporation.  All rights reserved.
// **************************************************************

// Simple struct for the (a,b,c,d) which is used to compute the mesage digest.    
internal struct ABCDStruct
{
    public uint A;
    public uint B;
    public uint C;
    public uint D;
}

public sealed class MD5Core
{
    //Prevent CSC from adding a default public constructor
    private MD5Core() {}

    public static byte[] GetHash(string input, Encoding encoding)
    {
        if (null == input)
        {
            throw new ArgumentNullException("input", "Unable to calculate hash over null input data");
        }
        if (null == encoding)
        {
            throw new ArgumentNullException("encoding", "Unable to calculate hash over a string without a default encoding. Consider using the GetHash(string) overload to use UTF8 Encoding");
        }

        var target = encoding.GetBytes(input);

        return GetHash(target);
    }

    public static byte[] GetHash(string input)
    {
        return GetHash(input, new UTF8Encoding());
    }

    public static string GetHashString(byte[] input)
    {
        if (null == input)
        {
            throw new ArgumentNullException("input", "Unable to calculate hash over null input data");
        }

        var retval = BitConverter.ToString(GetHash(input));
        retval = retval.Replace("-", "");

        return retval;
    }

    public static string GetHashString(string input, Encoding encoding)
    {
        if (null == input)
        {
            throw new ArgumentNullException("input", "Unable to calculate hash over null input data");
        }
        if (null == encoding)
        {
            throw new ArgumentNullException("encoding", "Unable to calculate hash over a string without a default encoding. Consider using the GetHashString(string) overload to use UTF8 Encoding");
        }

        var target = encoding.GetBytes(input);

        return GetHashString(target);
    }

    public static string GetHashString(string input)
    {
        return GetHashString(input, new UTF8Encoding());
    }

    public static byte[] GetHash(byte[] input)
    {
        if (null == input)
        {
            throw new ArgumentNullException("input", "Unable to calculate hash over null input data");
        }

        //Intitial values defined in RFC 1321
        var abcd = new ABCDStruct();
        abcd.A = 0x67452301;
        abcd.B = 0xefcdab89;
        abcd.C = 0x98badcfe;
        abcd.D = 0x10325476;

        //We pass in the input array by block, the final block of data must be handled specialy for padding & length embeding
        var startIndex = 0;
        while (startIndex <= input.Length - 64)
        {
            GetHashBlock(input, ref abcd, startIndex);
            startIndex += 64;
        }
        // The final data block. 
        return GetHashFinalBlock(input, startIndex, input.Length - startIndex, abcd, (Int64) input.Length*8);
    }

    internal static byte[] GetHashFinalBlock(byte[] input, int ibStart, int cbSize, ABCDStruct ABCD, Int64 len)
    {
        var working = new byte[64];
        var length = BitConverter.GetBytes(len);

        //Padding is a single bit 1, followed by the number of 0s required to make size congruent to 448 modulo 512. Step 1 of RFC 1321  
        //The CLR ensures that our buffer is 0-assigned, we don't need to explicitly set it. This is why it ends up being quicker to just
        //use a temporary array rather then doing in-place assignment (5% for small inputs)
        Array.Copy(input, ibStart, working, 0, cbSize);
        working[cbSize] = 0x80;

        //We have enough room to store the length in this chunk
        if (cbSize < 56)
        {
            Array.Copy(length, 0, working, 56, 8);
            GetHashBlock(working, ref ABCD, 0);
        }
        else //We need an aditional chunk to store the length
        {
            GetHashBlock(working, ref ABCD, 0);
            //Create an entirely new chunk due to the 0-assigned trick mentioned above, to avoid an extra function call clearing the array
            working = new byte[64];
            Array.Copy(length, 0, working, 56, 8);
            GetHashBlock(working, ref ABCD, 0);
        }
        var output = new byte[16];
        Array.Copy(BitConverter.GetBytes(ABCD.A), 0, output, 0, 4);
        Array.Copy(BitConverter.GetBytes(ABCD.B), 0, output, 4, 4);
        Array.Copy(BitConverter.GetBytes(ABCD.C), 0, output, 8, 4);
        Array.Copy(BitConverter.GetBytes(ABCD.D), 0, output, 12, 4);
        return output;
    }

    // Performs a single block transform of MD5 for a given set of ABCD inputs
    /* If implementing your own hashing framework, be sure to set the initial ABCD correctly according to RFC 1321:
    //    A = 0x67452301;
    //    B = 0xefcdab89;
    //    C = 0x98badcfe;
    //    D = 0x10325476;
    */

    internal static void GetHashBlock(byte[] input, ref ABCDStruct ABCDValue, int ibStart)
    {
        var temp = Converter(input, ibStart);
        var a = ABCDValue.A;
        var b = ABCDValue.B;
        var c = ABCDValue.C;
        var d = ABCDValue.D;

        a = r1(a, b, c, d, temp[0], 7, 0xd76aa478);
        d = r1(d, a, b, c, temp[1], 12, 0xe8c7b756);
        c = r1(c, d, a, b, temp[2], 17, 0x242070db);
        b = r1(b, c, d, a, temp[3], 22, 0xc1bdceee);
        a = r1(a, b, c, d, temp[4], 7, 0xf57c0faf);
        d = r1(d, a, b, c, temp[5], 12, 0x4787c62a);
        c = r1(c, d, a, b, temp[6], 17, 0xa8304613);
        b = r1(b, c, d, a, temp[7], 22, 0xfd469501);
        a = r1(a, b, c, d, temp[8], 7, 0x698098d8);
        d = r1(d, a, b, c, temp[9], 12, 0x8b44f7af);
        c = r1(c, d, a, b, temp[10], 17, 0xffff5bb1);
        b = r1(b, c, d, a, temp[11], 22, 0x895cd7be);
        a = r1(a, b, c, d, temp[12], 7, 0x6b901122);
        d = r1(d, a, b, c, temp[13], 12, 0xfd987193);
        c = r1(c, d, a, b, temp[14], 17, 0xa679438e);
        b = r1(b, c, d, a, temp[15], 22, 0x49b40821);

        a = r2(a, b, c, d, temp[1], 5, 0xf61e2562);
        d = r2(d, a, b, c, temp[6], 9, 0xc040b340);
        c = r2(c, d, a, b, temp[11], 14, 0x265e5a51);
        b = r2(b, c, d, a, temp[0], 20, 0xe9b6c7aa);
        a = r2(a, b, c, d, temp[5], 5, 0xd62f105d);
        d = r2(d, a, b, c, temp[10], 9, 0x02441453);
        c = r2(c, d, a, b, temp[15], 14, 0xd8a1e681);
        b = r2(b, c, d, a, temp[4], 20, 0xe7d3fbc8);
        a = r2(a, b, c, d, temp[9], 5, 0x21e1cde6);
        d = r2(d, a, b, c, temp[14], 9, 0xc33707d6);
        c = r2(c, d, a, b, temp[3], 14, 0xf4d50d87);
        b = r2(b, c, d, a, temp[8], 20, 0x455a14ed);
        a = r2(a, b, c, d, temp[13], 5, 0xa9e3e905);
        d = r2(d, a, b, c, temp[2], 9, 0xfcefa3f8);
        c = r2(c, d, a, b, temp[7], 14, 0x676f02d9);
        b = r2(b, c, d, a, temp[12], 20, 0x8d2a4c8a);

        a = r3(a, b, c, d, temp[5], 4, 0xfffa3942);
        d = r3(d, a, b, c, temp[8], 11, 0x8771f681);
        c = r3(c, d, a, b, temp[11], 16, 0x6d9d6122);
        b = r3(b, c, d, a, temp[14], 23, 0xfde5380c);
        a = r3(a, b, c, d, temp[1], 4, 0xa4beea44);
        d = r3(d, a, b, c, temp[4], 11, 0x4bdecfa9);
        c = r3(c, d, a, b, temp[7], 16, 0xf6bb4b60);
        b = r3(b, c, d, a, temp[10], 23, 0xbebfbc70);
        a = r3(a, b, c, d, temp[13], 4, 0x289b7ec6);
        d = r3(d, a, b, c, temp[0], 11, 0xeaa127fa);
        c = r3(c, d, a, b, temp[3], 16, 0xd4ef3085);
        b = r3(b, c, d, a, temp[6], 23, 0x04881d05);
        a = r3(a, b, c, d, temp[9], 4, 0xd9d4d039);
        d = r3(d, a, b, c, temp[12], 11, 0xe6db99e5);
        c = r3(c, d, a, b, temp[15], 16, 0x1fa27cf8);
        b = r3(b, c, d, a, temp[2], 23, 0xc4ac5665);

        a = r4(a, b, c, d, temp[0], 6, 0xf4292244);
        d = r4(d, a, b, c, temp[7], 10, 0x432aff97);
        c = r4(c, d, a, b, temp[14], 15, 0xab9423a7);
        b = r4(b, c, d, a, temp[5], 21, 0xfc93a039);
        a = r4(a, b, c, d, temp[12], 6, 0x655b59c3);
        d = r4(d, a, b, c, temp[3], 10, 0x8f0ccc92);
        c = r4(c, d, a, b, temp[10], 15, 0xffeff47d);
        b = r4(b, c, d, a, temp[1], 21, 0x85845dd1);
        a = r4(a, b, c, d, temp[8], 6, 0x6fa87e4f);
        d = r4(d, a, b, c, temp[15], 10, 0xfe2ce6e0);
        c = r4(c, d, a, b, temp[6], 15, 0xa3014314);
        b = r4(b, c, d, a, temp[13], 21, 0x4e0811a1);
        a = r4(a, b, c, d, temp[4], 6, 0xf7537e82);
        d = r4(d, a, b, c, temp[11], 10, 0xbd3af235);
        c = r4(c, d, a, b, temp[2], 15, 0x2ad7d2bb);
        b = r4(b, c, d, a, temp[9], 21, 0xeb86d391);

        ABCDValue.A = unchecked(a + ABCDValue.A);
        ABCDValue.B = unchecked(b + ABCDValue.B);
        ABCDValue.C = unchecked(c + ABCDValue.C);
        ABCDValue.D = unchecked(d + ABCDValue.D);
        return;
    }

    //Manually unrolling these equations nets us a 20% performance improvement
    private static uint r1(uint a, uint b, uint c, uint d, uint x, int s, uint t)
    {
        //                  (b + LSR((a + F(b, c, d) + x + t), s))
        //F(x, y, z)        ((x & y) | ((x ^ 0xFFFFFFFF) & z))
        return unchecked(b + LSR((a + ((b & c) | ((b ^ 0xFFFFFFFF) & d)) + x + t), s));
    }

    private static uint r2(uint a, uint b, uint c, uint d, uint x, int s, uint t)
    {
        //                  (b + LSR((a + G(b, c, d) + x + t), s))
        //G(x, y, z)        ((x & z) | (y & (z ^ 0xFFFFFFFF)))
        return unchecked(b + LSR((a + ((b & d) | (c & (d ^ 0xFFFFFFFF))) + x + t), s));
    }

    private static uint r3(uint a, uint b, uint c, uint d, uint x, int s, uint t)
    {
        //                  (b + LSR((a + H(b, c, d) + k + i), s))
        //H(x, y, z)        (x ^ y ^ z)
        return unchecked(b + LSR((a + (b ^ c ^ d) + x + t), s));
    }

    private static uint r4(uint a, uint b, uint c, uint d, uint x, int s, uint t)
    {
        //                  (b + LSR((a + I(b, c, d) + k + i), s))
        //I(x, y, z)        (y ^ (x | (z ^ 0xFFFFFFFF)))
        return unchecked(b + LSR((a + (c ^ (b | (d ^ 0xFFFFFFFF))) + x + t), s));
    }

    // Implementation of left rotate
    // s is an int instead of a uint becuase the CLR requires the argument passed to >>/<< is of 
    // type int. Doing the demoting inside this function would add overhead.
    private static uint LSR(uint i, int s)
    {
        return ((i << s) | (i >> (32 - s)));
    }

    //Convert input array into array of UInts
    private static uint[] Converter(byte[] input, int ibStart)
    {
        if (null == input)
        {
            throw new ArgumentNullException("input", "Unable convert null array to array of uInts");
        }

        var result = new uint[16];

        for (var i = 0; i < 16; i++)
        {
            result[i] = (uint) input[ibStart + i*4];
            result[i] += (uint) input[ibStart + i*4 + 1] << 8;
            result[i] += (uint) input[ibStart + i*4 + 2] << 16;
            result[i] += (uint) input[ibStart + i*4 + 3] << 24;
        }

        return result;
    }
}

/*
Different license terms apply to different file types:

- Source code files are governed by the MICROSOFT PUBLIC LICENSE (Ms-PL) (INCLUDED BELOW).
- Binary files are governed by MSDN CODE GALLERY BINARY LICENSE (INCLUDED BELOW). 
- Documentation files are governed by CREATIVE COMMONS ATTRIBUTION 3.0 LICENSE (INCLUDED BELOW).
 

MICROSOFT PUBLIC LICENSE (Ms-PL)

This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.

1. Definitions

The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
 
A "contribution" is the original software, or any additions or changes to the software.

A "contributor" is any person that distributes its contribution under this license. 

"Licensed patents" are a contributor's patent claims that read directly on its contribution. 

2. Grant of Rights

(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.

(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software. 

3. Conditions and Limitations

(A) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.

(B) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.

(C) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.

(D) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.

(E) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement. 


MSDN CODE GALLERY BINARY LICENSE 
 
You are free to install, use, copy and distribute any number of copies of the software, in object code form, provided that you retain:

• all copyright, patent, trademark, and attribution notices that are present in the software, 
• this list of conditions, and 
• the following disclaimer in the documentation and/or other materials provided with the software. 

The software is licensed “as-is.” You bear the risk of using it.  No express warranties, guarantees or conditions are provided. To the extent permitted under your local laws, the implied warranties of merchantability, fitness for a particular purpose and non-infringement are excluded.  

This license does not grant you any rights to use any other party’s name, logo, or trademarks. All rights not specifically granted herein are reserved. 

v061708
 

CREATIVE COMMONS ATTRIBUTION 3.0 LICENSE

THE WORK (AS DEFINED BELOW) IS PROVIDED UNDER THE TERMS OF THIS CREATIVE COMMONS PUBLIC LICENSE ("CCPL" OR "LICENSE"). THE WORK IS PROTECTED BY COPYRIGHT AND/OR OTHER APPLICABLE LAW. ANY USE OF THE WORK OTHER THAN AS AUTHORIZED UNDER THIS LICENSE OR COPYRIGHT LAW IS PROHIBITED.

BY EXERCISING ANY RIGHTS TO THE WORK PROVIDED HERE, YOU ACCEPT AND AGREE TO BE BOUND BY THE TERMS OF THIS LICENSE. TO THE EXTENT THIS LICENSE MAY BE CONSIDERED TO BE A CONTRACT, THE LICENSOR GRANTS YOU THE RIGHTS CONTAINED HERE IN CONSIDERATION OF YOUR ACCEPTANCE OF SUCH TERMS AND CONDITIONS.

1. Definitions

a. "Collective Work" means a work, such as a periodical issue, anthology or encyclopedia, in which the Work in its entirety in unmodified form, along with one or more other contributions, constituting separate and independent works in themselves, are assembled into a collective whole. A work that constitutes a Collective Work will not be considered a Derivative Work (as defined below) for the purposes of this License. 

b. "Derivative Work" means a work based upon the Work or upon the Work and other pre-existing works, such as a translation, musical arrangement, dramatization, fictionalization, motion picture version, sound recording, art reproduction, abridgment, condensation, or any other form in which the Work may be recast, transformed, or adapted, except that a work that constitutes a Collective Work will not be considered a Derivative Work for the purpose of this License. For the avoidance of doubt, where the Work is a musical composition or sound recording, the synchronization of the Work in timed-relation with a moving image ("synching") will be considered a Derivative Work for the purpose of this License. 

c. "Licensor" means the individual, individuals, entity or entities that offers the Work under the terms of this License. 

d. "Original Author" means the individual, individuals, entity or entities who created the Work. 

e. "Work" means the copyrightable work of authorship offered under the terms of this License. 

f. "You" means an individual or entity exercising rights under this License who has not previously violated the terms of this License with respect to the Work, or who has received express permission from the Licensor to exercise rights under this License despite a previous violation. 

2. Fair Use Rights. Nothing in this license is intended to reduce, limit, or restrict any rights arising from fair use, first sale or other limitations on the exclusive rights of the copyright owner under copyright law or other applicable laws.

3. License Grant. Subject to the terms and conditions of this License, Licensor hereby grants You a worldwide, royalty-free, non-exclusive, perpetual (for the duration of the applicable copyright) license to exercise the rights in the Work as stated below:

a. to reproduce the Work, to incorporate the Work into one or more Collective Works, and to reproduce the Work as incorporated in the Collective Works; 

b. to create and reproduce Derivative Works provided that any such Derivative Work, including any translation in any medium, takes reasonable steps to clearly label, demarcate or otherwise identify that changes were made to the original Work. For example, a translation could be marked "The original work was translated from English to Spanish," or a modification could indicate "The original work has been modified.";; 

c. to distribute copies or phonorecords of, display publicly, perform publicly, and perform publicly by means of a digital audio transmission the Work including as incorporated in Collective Works; 

d. to distribute copies or phonorecords of, display publicly, perform publicly, and perform publicly by means of a digital audio transmission Derivative Works. 

e. For the avoidance of doubt, where the Work is a musical composition:

i. Performance Royalties Under Blanket Licenses. Licensor waives the exclusive right to collect, whether individually or, in the event that Licensor is a member of a performance rights society (e.g. ASCAP, BMI, SESAC), via that society, royalties for the public performance or public digital performance (e.g. webcast) of the Work. 

ii. Mechanical Rights and Statutory Royalties. Licensor waives the exclusive right to collect, whether individually or via a music rights agency or designated agent (e.g. Harry Fox Agency), royalties for any phonorecord You create from the Work ("cover version") and distribute, subject to the compulsory license created by 17 USC Section 115 of the US Copyright Act (or the equivalent in other jurisdictions). 

f. Webcasting Rights and Statutory Royalties. For the avoidance of doubt, where the Work is a sound recording, Licensor waives the exclusive right to collect, whether individually or via a performance-rights society (e.g. SoundExchange), royalties for the public digital performance (e.g. webcast) of the Work, subject to the compulsory license created by 17 USC Section 114 of the US Copyright Act (or the equivalent in other jurisdictions). 

The above rights may be exercised in all media and formats whether now known or hereafter devised. The above rights include the right to make such modifications as are technically necessary to exercise the rights in other media and formats. All rights not expressly granted by Licensor are hereby reserved.

4. Restrictions. The license granted in Section 3 above is expressly made subject to and limited by the following restrictions:

a. You may distribute, publicly display, publicly perform, or publicly digitally perform the Work only under the terms of this License, and You must include a copy of, or the Uniform Resource Identifier for, this License with every copy or phonorecord of the Work You distribute, publicly display, publicly perform, or publicly digitally perform. You may not offer or impose any terms on the Work that restrict the terms of this License or the ability of a recipient of the Work to exercise the rights granted to that recipient under the terms of the License. You may not sublicense the Work. You must keep intact all notices that refer to this License and to the disclaimer of warranties. When You distribute, publicly display, publicly perform, or publicly digitally perform the Work, You may not impose any technological measures on the Work that restrict the ability of a recipient of the Work from You to exercise the rights granted to that recipient under the terms of the License. This Section 4(a) applies to the Work as incorporated in a Collective Work, but this does not require the Collective Work apart from the Work itself to be made subject to the terms of this License. If You create a Collective Work, upon notice from any Licensor You must, to the extent practicable, remove from the Collective Work any credit as required by Section 4(b), as requested. If You create a Derivative Work, upon notice from any Licensor You must, to the extent practicable, remove from the Derivative Work any credit as required by Section 4(b), as requested. 

b. If You distribute, publicly display, publicly perform, or publicly digitally perform the Work (as defined in Section 1 above) or any Derivative Works (as defined in Section 1 above) or Collective Works (as defined in Section 1 above), You must, unless a request has been made pursuant to Section 4(a), keep intact all copyright notices for the Work and provide, reasonable to the medium or means You are utilizing: (i) the name of the Original Author (or pseudonym, if applicable) if supplied, and/or (ii) if the Original Author and/or Licensor designate another party or parties (e.g. a sponsor institute, publishing entity, journal) for attribution ("Attribution Parties") in Licensor's copyright notice, terms of service or by other reasonable means, the name of such party or parties; the title of the Work if supplied; to the extent reasonably practicable, the Uniform Resource Identifier, if any, that Licensor specifies to be associated with the Work, unless such URI does not refer to the copyright notice or licensing information for the Work; and, consistent with Section 3(b) in the case of a Derivative Work, a credit identifying the use of the Work in the Derivative Work (e.g., "French translation of the Work by Original Author," or "Screenplay based on original Work by Original Author"). The credit required by this Section 4(b) may be implemented in any reasonable manner; provided, however, that in the case of a Derivative Work or Collective Work, at a minimum such credit will appear, if a credit for all contributing authors of the Derivative Work or Collective Work appears, then as part of these credits and in a manner at least as prominent as the credits for the other contributing authors. For the avoidance of doubt, You may only use the credit required by this Section for the purpose of attribution in the manner set out above and, by exercising Your rights under this License, You may not implicitly or explicitly assert or imply any connection with, sponsorship or endorsement by the Original Author, Licensor and/or Attribution Parties, as appropriate, of You or Your use of the Work, without the separate, express prior written permission of the Original Author, Licensor and/or Attribution Parties. 

5. Representations, Warranties and Disclaimer

UNLESS OTHERWISE MUTUALLY AGREED TO BY THE PARTIES IN WRITING, LICENSOR OFFERS THE WORK AS-IS AND ONLY TO THE EXTENT OF ANY RIGHTS HELD IN THE LICENSED WORK BY THE LICENSOR. THE LICENSOR MAKES NO REPRESENTATIONS OR WARRANTIES OF ANY KIND CONCERNING THE WORK, EXPRESS, IMPLIED, STATUTORY OR OTHERWISE, INCLUDING, WITHOUT LIMITATION, WARRANTIES OF TITLE, MARKETABILITY, MERCHANTIBILITY, FITNESS FOR A PARTICULAR PURPOSE, NONINFRINGEMENT, OR THE ABSENCE OF LATENT OR OTHER DEFECTS, ACCURACY, OR THE PRESENCE OF ABSENCE OF ERRORS, WHETHER OR NOT DISCOVERABLE. SOME JURISDICTIONS DO NOT ALLOW THE EXCLUSION OF IMPLIED WARRANTIES, SO SUCH EXCLUSION MAY NOT APPLY TO YOU.

6. Limitation on Liability. EXCEPT TO THE EXTENT REQUIRED BY APPLICABLE LAW, IN NO EVENT WILL LICENSOR BE LIABLE TO YOU ON ANY LEGAL THEORY FOR ANY SPECIAL, INCIDENTAL, CONSEQUENTIAL, PUNITIVE OR EXEMPLARY DAMAGES ARISING OUT OF THIS LICENSE OR THE USE OF THE WORK, EVEN IF LICENSOR HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.

7. Termination

a. This License and the rights granted hereunder will terminate automatically upon any breach by You of the terms of this License. Individuals or entities who have received Derivative Works (as defined in Section 1 above) or Collective Works (as defined in Section 1 above) from You under this License, however, will not have their licenses terminated provided such individuals or entities remain in full compliance with those licenses. Sections 1, 2, 5, 6, 7, and 8 will survive any termination of this License. 

b. Subject to the above terms and conditions, the license granted here is perpetual (for the duration of the applicable copyright in the Work). Notwithstanding the above, Licensor reserves the right to release the Work under different license terms or to stop distributing the Work at any time; provided, however that any such election will not serve to withdraw this License (or any other license that has been, or is required to be, granted under the terms of this License), and this License will continue in full force and effect unless terminated as stated above. 

8. Miscellaneous

a. Each time You distribute or publicly digitally perform the Work (as defined in Section 1 above) or a Collective Work (as defined in Section 1 above), the Licensor offers to the recipient a license to the Work on the same terms and conditions as the license granted to You under this License. 

b. Each time You distribute or publicly digitally perform a Derivative Work, Licensor offers to the recipient a license to the original Work on the same terms and conditions as the license granted to You under this License. 

c. If any provision of this License is invalid or unenforceable under applicable law, it shall not affect the validity or enforceability of the remainder of the terms of this License, and without further action by the parties to this agreement, such provision shall be reformed to the minimum extent necessary to make such provision valid and enforceable. 

d. No term or provision of this License shall be deemed waived and no breach consented to unless such waiver or consent shall be in writing and signed by the party to be charged with such waiver or consent. 

e. This License constitutes the entire agreement between the parties with respect to the Work licensed here. There are no understandings, agreements or representations with respect to the Work not specified here. Licensor shall not be bound by any additional provisions that may appear in any communication from You. This License may not be modified without the mutual written agreement of the Licensor and You.
*/