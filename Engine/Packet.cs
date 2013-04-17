using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

public class Packet
{

    private List<byte> buffer;
    private uint bitIndex = 0;
    private uint packetErrors = 0;

    //Constructor
    public Packet()
    {
        bitIndex = 0;
        buffer = new List<byte>();
    }

    public Packet(byte[] buffer_)
    {
        bitIndex = 0;
        buffer = new List<byte>(buffer_);
    }

    //WRITE FUNCTIONS

    //Writes a Point

    public void WritePoint(Point point)
    {
        WriteSignedInt(point.X);
        WriteSignedInt(point.Y);
    }

    //Writes an Event ID
    public void WriteEventID(uint value)
    {
        WriteDynamicUnsignedNbitInteger(value, 6);
    }

    //Writes a single bit either 0 or 1 onto the buffer
    public void WriteBoolean(Boolean value)
    {
        while ((int)((bitIndex + 8) / 8) > buffer.Count) buffer.Add(new byte());
        buffer[(int)(bitIndex / 8)] |= (byte)((value == true ? 1 : 0) << (int)(7 - (bitIndex) % 8));
        ++bitIndex;
    }

    //Writes an 8 bit unsigned byte onto the buffer
    public void WriteUnsignedByte(byte value)
    {
        uint dataTypeSize = 1;//byte size
        while ((int)((bitIndex + dataTypeSize * 8) / 8) >= buffer.Count) buffer.Add(new byte());
        for (uint copyBits = 0; copyBits < dataTypeSize * 8; ++copyBits)
        {
            buffer[(int)((bitIndex + copyBits) / 8)] |= (byte)(((value >> (int)((dataTypeSize * 8 - 1) - copyBits)) & 0x1) << (int)(7 - (bitIndex + copyBits) % 8));
        }
        bitIndex += dataTypeSize * 8;
    }

    //Writes an 8 bit signed byte onto the buffer
    public void WriteSignedByte(sbyte value)
    {
        uint dataTypeSize = 1;//byte size
        while ((int)((bitIndex + dataTypeSize * 8) / 8) >= buffer.Count) buffer.Add(new byte());
        for (uint copyBits = 0; copyBits < dataTypeSize * 8; ++copyBits)
        {
            buffer[(int)((bitIndex + copyBits) / 8)] |= (byte)(((value >> (int)((dataTypeSize * 8 - 1) - copyBits)) & 0x1) << (int)(7 - (bitIndex + copyBits) % 8));
        }
        bitIndex += dataTypeSize * 8;
    }

    //Writes a 16 bit unsigned short onto the buffer
    public void WriteUnsignedShort(ushort value)
    {
        uint dataTypeSize = 2;//byte size
        while ((int)((bitIndex + dataTypeSize * 8) / 8) >= buffer.Count) buffer.Add(new byte());
        for (uint copyBits = 0; copyBits < dataTypeSize * 8; ++copyBits)
        {
            buffer[(int)((bitIndex + copyBits) / 8)] |= (byte)(((value >> (int)((dataTypeSize * 8 - 1) - copyBits)) & 0x1) << (int)(7 - (bitIndex + copyBits) % 8));
        }
        bitIndex += dataTypeSize * 8;
    }

    //Writes a 16 bit signed short onto the buffer
    public void WriteSignedShort(short value)
    {
        uint dataTypeSize = 2;//byte size
        while ((int)((bitIndex + dataTypeSize * 8) / 8) >= buffer.Count) buffer.Add(new byte());
        for (uint copyBits = 0; copyBits < dataTypeSize * 8; ++copyBits)
        {
            buffer[(int)((bitIndex + copyBits) / 8)] |= (byte)(((value >> (int)((dataTypeSize * 8 - 1) - copyBits)) & 0x1) << (int)(7 - (bitIndex + copyBits) % 8));
        }
        bitIndex += dataTypeSize * 8;
    }

    //Writes a 32 bit unsigned integer onto the buffer
    public void WriteUnsignedInt(uint value)
    {
        uint dataTypeSize = 4;//byte size
        while ((int)((bitIndex + dataTypeSize * 8) / 8) >= buffer.Count) buffer.Add(new byte());
        for (uint copyBits = 0; copyBits < dataTypeSize * 8; ++copyBits)
        {
            buffer[(int)((bitIndex + copyBits) / 8)] |= (byte)(((value >> (int)((dataTypeSize * 8 - 1) - copyBits)) & 0x1) << (int)(7 - (bitIndex + copyBits) % 8));
        }
        bitIndex += dataTypeSize * 8;
    }

    //Writes a 32 bit signed integer onto the buffer
    public void WriteSignedInt(int value)
    {
        uint dataTypeSize = 4;//byte size
        while ((int)((bitIndex + dataTypeSize * 8) / 8) >= buffer.Count) buffer.Add(new byte());
        for (uint copyBits = 0; copyBits < dataTypeSize * 8; ++copyBits)
        {
            buffer[(int)((bitIndex + copyBits) / 8)] |= (byte)(((value >> (int)((dataTypeSize * 8 - 1) - copyBits)) & 0x1) << (int)(7 - (bitIndex + copyBits) % 8));
        }
        bitIndex += dataTypeSize * 8;
    }

    //Writes an n bit unsigned integer onto the buffer
    public void WriteCustomUnsignedInteger(uint value, uint bits)
    {
        while ((int)((bitIndex + bits) / 8) >= buffer.Count) buffer.Add(new byte());
        for (uint copyBits = 0; copyBits < bits; ++copyBits)
        {
            buffer[(int)((bitIndex + copyBits) / 8)] |= (byte)(((value >> (int)((bits - 1) - copyBits)) & 0x1) << (int)(7 - (bitIndex + copyBits) % 8));
        }
        bitIndex += bits;
    }

    //Writes an n bit signed integer onto the buffer
    public void WriteCustomSignedInteger(int value, uint bits)
    {
        while ((int)((bitIndex + bits) / 8) >= buffer.Count) buffer.Add(new byte());
        for (uint copyBits = 0; copyBits < bits; ++copyBits)
        {
            buffer[(int)((bitIndex + copyBits) / 8)] |= (byte)(((value >> (int)((bits - 1) - copyBits)) & 0x1) << (int)(7 - (bitIndex + copyBits) % 8));
        }
        bitIndex += bits;
    }

    //Writes a 32 bit floating point number onto the buffer
    public void WriteFloat(float value)
    {
        WriteUnsignedInt(BitConverter.ToUInt32(BitConverter.GetBytes(value), 0));
    }

    //Writes a 64 bit floating point number onto the buffer
    public void WriteDouble(double value)
    {
        byte[] bytes = BitConverter.GetBytes(value);
        WriteUnsignedInt(BitConverter.ToUInt32(bytes, 0));
        WriteUnsignedInt(BitConverter.ToUInt32(bytes, 4));
    }

    //Writes a number by writing intervals of 2^arrayBitInterval-1, the value on the interval of bits equal to 2^arrayBitInterval means to look onto the next interval
    //Example, let arrayBitInterval = 4 and value = 100, 2^4-1 is 15, so 100 % 15 is 6 sets of 4 bits set to 1111 with 10 left over so 1010 would be the 7th set. Good for small values that rarely go over 2^arrayBitInterval-1
    public void WriteDynamicUnsignedInteger(uint value, uint arrayBitInterval)
    {
        uint interval = (uint)((0x1 << (int)arrayBitInterval) - 1);
        uint intervalCount = (uint)(Math.Floor((double)(value / interval)));
        uint copyBits;
        for (uint copyInterval = 0; copyInterval < intervalCount; ++copyInterval)
        {
            for (copyBits = 0; copyBits < arrayBitInterval; ++copyBits)
            {
                while ((int)((bitIndex + copyBits) / 8) >= buffer.Count) buffer.Add(new byte());
                buffer[(int)((bitIndex + copyBits) / 8)] |= (byte)(0x1 << (int)(7 - (bitIndex + copyBits) % 8));
            }
            bitIndex += arrayBitInterval;
        }
        value -= intervalCount * interval;
        for (copyBits = 0; copyBits < arrayBitInterval; ++copyBits)
        {
            while ((int)((bitIndex + copyBits) / 8) >= buffer.Count) buffer.Add(new byte());
            buffer[(int)((bitIndex + copyBits) / 8)] |= (byte)(((value >> (int)((arrayBitInterval - 1) - copyBits)) & 0x1) << (int)(7 - (bitIndex + copyBits) % 8));
        }
        bitIndex += arrayBitInterval;
    }

    //Writes a number by writing intervals of 2^arrayBitInterval-1, the value on the interval of bits equal to 2^arrayBitInterval means to look onto the next interval
    //The sign is copied first as 0 is negative and 1 is positive. The value is then copied as the absolute value
    //Example, let arrayBitInterval = 4 and value = -100, 2^4-1 is 15, so 100 % 15 is 6 sets of 4 bits set to 1111 with 10 left over so 1010 would be the 7th set. Good for small values that rarely go over 2^arrayBitInterval-1, a 0 bit is copied first
    public void WriteDynamicSignedInteger(int value, uint arrayBitInterval)
    {
        WriteBoolean(value > 0 ? true : false);
        WriteDynamicUnsignedInteger((uint)Math.Abs(value), arrayBitInterval);
    }

    //Writes the number of bits and an extra bit to tell to go onto the next sequence of bits.
    //In this way the bits for a number are broken up. Smaller numbers take up less space
    public void WriteDynamicUnsignedNbitInteger(uint value, uint bits)
    {
        uint offset = 0;
        uint copyBits;
        for (copyBits = 0; (copyBits - offset < 32 ? (0x1 << (int)(copyBits - offset)) : Math.Pow(2, copyBits - offset)) - 1 <= value || copyBits % bits != 0; ++copyBits)
        {
            while ((int)((bitIndex + copyBits + offset) / 8) >= buffer.Count) buffer.Add(new byte());
            buffer[(int)((bitIndex + copyBits + offset) / 8)] |= (byte)(((value >> (int)copyBits) & 0x1) << (byte)(7 - (bitIndex + copyBits + offset) % 8));
            //trace("bit-" + (bitIndex+copyBits+offset) + " = " + ((value >> copyBits) & 0x1));
            if ((copyBits + 1) % (bits) == 0 && copyBits != 0)
            {
                offset++;
                while ((int)((bitIndex + copyBits + offset) / 8) >= buffer.Count) buffer.Add(new byte());
                //if ((uint)(0x1 << (int)(copyBits + 1)) - 1 < value)
                uint powerOfTwo = (uint)(copyBits - offset < 32 ? (0x1 << (int)(copyBits - offset + 1)) : Math.Pow(2, copyBits - offset + 1)) - 1;
                if (powerOfTwo < value)
                {
                    buffer[(int)((bitIndex + copyBits + offset) / 8)] |= (byte)(0x1 << (int)(7 - (bitIndex + copyBits + offset) % 8));
                    //trace("-bit-" + (bitIndex+copyBits+offset) + " = 1");
                }//else{trace("-bit-" + (bitIndex+copyBits+offset) + " = 0");}
            }
        }
        bitIndex += copyBits + offset;
    }

    public void WriteDynamicUnsignedNbitInteger(uint value)
    {
        WriteDynamicUnsignedNbitInteger(value, 4);
    }

    //Writes the number of bits and an extra bit to tell to go onto the next sequence of bits.
    //In this way the bits for a number are broken up. Smaller numbers take up less space
    //The sign is represented as a bit either 0 to mean negative or 1 to mean positive
    public void WriteDynamicSignedNbitInteger(int value, uint bits)
    {
        WriteBoolean(value > 0 ? true : false);
        WriteDynamicUnsignedNbitInteger((uint)Math.Abs(value), bits);
    }

    public void WriteDynamicSignedNbitInteger(int value)
    {
        WriteDynamicSignedNbitInteger(value, 4);
    }

    //Creates a value for the ratio value/2^bitResolution-1 in relationship to value/(max-min)
    //This allows a floating point to have a resolution
    public void WriteCustomResolutionFloat(float min, float max, float value, uint bitResolution)
    {
        WriteCustomUnsignedInteger((uint)Math.Round((value - min) / (max - min) * (float)((0x1 << (int)bitResolution) - 1)), bitResolution);
    }

    //Creates a value for the ratio value/2^bitResolution-1 in relationship to value/(max-min)
    //This allows a floating point to have a resolution
    public void WriteCustomResolutionDouble(double min, double max, double value, uint bitResolution)
    {
        WriteCustomUnsignedInteger((uint)Math.Round((value - min) / (max - min) * (Double)((0x1 << (int)bitResolution) - 1)), bitResolution);
    }

    /*
    //Writes the length of the string using WriteDynamicUnsignedNbitInteger and then writes 7 bit characters using the ascii values 0-126
    public void WriteString(string value, uint arrayBits)
    {
        uint size = (uint)value.Length;
        uint tempValue = 0;
        WriteDynamicUnsignedNbitInteger(size, arrayBits);
        for (int stringItr = 0; stringItr < size; ++stringItr)
        {
            tempValue = (uint)(value[stringItr]) - 32;
            //trace("Character: " + stringItr + " " + tempValue);
            if (tempValue >= 0 && tempValue <= 128)
            {
                WriteCustomUnsignedInteger(tempValue, 7);
            }
        }
    }
    */

    public void WriteString(string value, uint arrayBits)
    {
        uint size = (uint)value.Length;
        WriteDynamicUnsignedNbitInteger(size, arrayBits);

        ASCIIEncoding e = new ASCIIEncoding();
        byte[] data = e.GetBytes(value);
        foreach (byte d in data)
        {
            WriteUnsignedByte(d);
        }
    }

    public void WriteString(string value)
    {
        WriteString(value, 4);
    }

    //WRITE FUNCTIONS - ARRAYS

    //Writes an array with a size written with a WriteDynamicUnsignedNbitInteger before the unsigned integers
    public void WriteUnsignedCustomIntegerArray(List<uint> value, uint bits, uint arrayBits)
    {//arrayBitInterval means that for value < 2^arrayBitInterval use the number of bits defined by arrayBitInterval
        uint size = (uint)value.Count;
        WriteDynamicUnsignedNbitInteger(size, arrayBits);
        for (int cycleArray = 0; cycleArray < size; ++cycleArray)
        {
            WriteCustomUnsignedInteger(value[cycleArray], bits);
        }
    }

    //Writes an array with a size written with a WriteDynamicUnsignedNbitInteger before the signed integers
    public void WriteSignedCustomIntegerArray(List<int> value, uint bits, uint arrayBits)
    {//arrayBitInterval means that for value < 2^arrayBitInterval use the number of bits defined by arrayBitInterval
        uint size = (uint)value.Count;
        WriteDynamicUnsignedNbitInteger(size, arrayBits);
        for (int cycleArray = 0; cycleArray < size; ++cycleArray)
        {
            WriteCustomSignedInteger(value[cycleArray], bits);
        }
    }

    //Write a float array with the size written with a WriteDynamicUnsignedNbitInteger before the floats
    public void WriteFloatArray(List<float> value, uint arrayBits)
    {
        uint size = (uint)value.Count;
        WriteDynamicUnsignedNbitInteger(size, arrayBits);
        for (int cycleArray = 0; cycleArray < size; ++cycleArray)
        {
            WriteFloat(value[cycleArray]);
        }
    }

    //Write a double array with the size written with a WriteDynamicUnsignedNbitInteger before the doubles
    public void WriteDoubleArray(List<double> value, uint arrayBits)
    {
        uint size = (uint)value.Count;
        WriteDynamicUnsignedNbitInteger(size, arrayBits);
        for (int cycleArray = 0; cycleArray < size; ++cycleArray)
        {
            WriteDouble(value[cycleArray]);
        }
    }

    //Write a custom float resolution array with the size written with a WriteDynamicUnsignedNbitInteger before the custom resolution floats
    public void WriteCustomResolutionFloatArray(float min, float max, List<float> value, uint bitResolution, uint arrayBits)
    {
        uint size = (uint)value.Count;
        WriteDynamicUnsignedNbitInteger(size, arrayBits);
        for (int cycleArray = 0; cycleArray < size; ++cycleArray)
        {
            WriteCustomResolutionFloat(min, max, value[cycleArray], bitResolution);
        }
    }

    //Write a custom double resolution array with the size written with a WriteDynamicUnsignedNbitInteger before the custom resolution doubles
    public void WriteCustomResolutionDoubleArray(double min, double max, List<double> value, uint bitResolution, uint arrayBits)
    {
        uint size = (uint)value.Count;
        WriteDynamicUnsignedNbitInteger(size, arrayBits);
        for (int cycleArray = 0; cycleArray < size; ++cycleArray)
        {
            WriteCustomResolutionDouble(min, max, value[cycleArray], bitResolution);
        }
    }

    //WRITE FUNCTIONS - ASSOCIATIVE ARRAYS

    //bits is the the number of bits for the unsigned integer and arrayBits sets the maximum element size 2^arrayBits
    public void WriteAssociativeArrayStringToUnsignedInteger(Dictionary<string, uint> value, uint bits, uint arrayBits)
    {
        uint sizeBitIndex = bitIndex;
        uint size = 0;
        bitIndex += arrayBits;
        foreach (KeyValuePair<string, uint> kvp in value)
        {
            WriteString(kvp.Key, 5);
            WriteCustomUnsignedInteger(kvp.Value, bits);
            size++;
        }
        uint tempBitIndex = bitIndex;
        bitIndex = sizeBitIndex;
        WriteCustomUnsignedInteger(size, arrayBits);
        bitIndex = tempBitIndex;
    }

    //bits is the the number of bits for the unsigned integer and arrayBits sets the maximum element size 2^arrayBits
    public void WriteAssociativeArrayStringToSignedInteger(Dictionary<string, int> value, uint bits, uint arrayBits)
    {
        uint sizeBitIndex = bitIndex;
        uint size = 0;
        bitIndex += arrayBits;
        foreach (KeyValuePair<string, int> kvp in value)
        {
            WriteString(kvp.Key, 5);
            WriteCustomSignedInteger(kvp.Value, bits);
            size++;
        }
        uint tempBitIndex = bitIndex;
        bitIndex = sizeBitIndex;
        WriteCustomUnsignedInteger(size, arrayBits);
        bitIndex = tempBitIndex;
    }

    //arrayBits sets the maximum element size 2^arrayBits
    public void WriteAssociativeArrayStringToFloat(Dictionary<string, float> value, uint arrayBits)
    {
        uint sizeBitIndex = bitIndex;
        uint size = 0;
        bitIndex += arrayBits;
        foreach (KeyValuePair<string, float> kvp in value)
        {
            WriteString(kvp.Key, 5);
            WriteFloat(kvp.Value);
            size++;
        }
        uint tempBitIndex = bitIndex;
        bitIndex = sizeBitIndex;
        WriteCustomUnsignedInteger(size, arrayBits);
        bitIndex = tempBitIndex;
    }

    //arrayBits sets the maximum element size 2^arrayBits
    public void WriteAssociativeArrayStringToDouble(Dictionary<string, double> value, uint arrayBits)
    {
        uint sizeBitIndex = bitIndex;
        uint size = 0;
        bitIndex += arrayBits;
        foreach (KeyValuePair<string, double> kvp in value)
        {
            WriteString(kvp.Key, 5);
            WriteDouble(kvp.Value);
            size++;
        }
        uint tempBitIndex = bitIndex;
        bitIndex = sizeBitIndex;
        WriteCustomUnsignedInteger(size, arrayBits);
        bitIndex = tempBitIndex;
    }

    //arrayBits sets the maximum element size 2^arrayBits
    public void WriteAssociativeArrayStringToString(Dictionary<string, string> value, uint arrayBits)
    {
        uint sizeBitIndex = bitIndex;
        uint size = 0;
        bitIndex += arrayBits;
        foreach (KeyValuePair<string, string> kvp in value)
        {
            WriteString(kvp.Key, 5);
            WriteString(kvp.Value, 5);
            size++;
        }
        uint tempBitIndex = bitIndex;
        bitIndex = sizeBitIndex;
        WriteCustomUnsignedInteger(size, arrayBits);
        bitIndex = tempBitIndex;
    }

    //WRITE BINARY PACKET

    public void WritePacket(Packet packet_)
    {
        uint oldBitIndex = packet_.GetBitIndex();
        packet_.ResetBitIndex();
        uint packetSize = packet_.Length();//byte size
        WriteDynamicUnsignedNbitInteger(packetSize);
        for (uint copyBytes = 0; copyBytes < packetSize; ++copyBytes)
        {
            WriteUnsignedByte(packet_.ReadUnsignedByte());
        }
        bitIndex += packetSize * 8;
        SetBitIndex(oldBitIndex);
    }

    //READ FUNCTIONS

    //Read a point
    public Point ReadPoint()
    {
        return new Point(ReadSignedInt(), ReadSignedInt());
    }

    //Reads an Event ID
    public uint ReadEventID()
    {
        return ReadDynamicUnsignedNbitInteger(6);
    }

    //Reads one bit from the buffer either a 0 or a 1
    public Boolean ReadBoolean()
    {
        Boolean value;
        if ((int)((bitIndex + 1) / 8) < buffer.Count)
        {
            value = ((buffer[(int)(bitIndex / 8)] >> (int)(7 - bitIndex % 8)) & 0x1) == 1;
            ++bitIndex;
        }
        else
        {
            packetErrors++;
            return true;
        }
        return value;
    }

    //Reads an 8 bits unsigned byte from the buffer
    public byte ReadUnsignedByte()
    {
        byte value;
        if ((int)((bitIndex + 8) / 8) < buffer.Count)
        {
            uint dataTypeSize = 1;//byte size
            value = 0x0;
            for (uint copyBits = 0; copyBits < dataTypeSize * 8; ++copyBits)
            {
                value <<= 1;
                value |= (byte)((buffer[(int)((bitIndex + copyBits) / 8)] >> (int)(7 - (bitIndex + copyBits) % 8)) & 0x1);
            }
            bitIndex += dataTypeSize * 8;
        }
        else
        {
            packetErrors++;
            return 0;
        }
        return value;
    }

    //Reads an 8 bits signed byte from the buffer
    public sbyte ReadSignedByte()
    {
        sbyte value;
        if ((int)((bitIndex + 8) / 8) < buffer.Count)
        {
            uint dataTypeSize = 1;//byte size
            value = 0;
            value |= (sbyte)(((buffer[(int)(bitIndex / 8)] >> (int)(7 - bitIndex % 8)) & 0x1) == 1 ? 0xFF : 0x0);
            for (uint copyBits = 0; copyBits < dataTypeSize * 8; ++copyBits)
            {
                value <<= 1;
                value |= (sbyte)((buffer[(int)((bitIndex + copyBits) / 8)] >> (int)(7 - (bitIndex + copyBits) % 8)) & 0x1);
            }
            bitIndex += dataTypeSize * 8;
        }
        else
        {
            packetErrors++;
            return 0;
        }
        return value;
    }

    //Reads a 16 bit unsigned short from the buffer
    public ushort ReadUnsignedShort()
    {
        ushort value;
        if ((int)((bitIndex + 16) / 8) < buffer.Count)
        {
            uint dataTypeSize = 2;//byte size
            value = 0x0;
            for (uint copyBits = 0; copyBits < dataTypeSize * 8; ++copyBits)
            {
                value <<= 1;
                value |= (byte)((buffer[(int)((bitIndex + copyBits) / 8)] >> (int)(7 - (bitIndex + copyBits) % 8)) & 0x1);
            }
            bitIndex += dataTypeSize * 8;
        }
        else
        {
            packetErrors++;
            return 0;
        }
        return value;
    }

    //Reads a 16 bit signed short from the buffer
    public short ReadSignedShort()
    {
        short value;
        if ((int)((bitIndex + 16) / 8) < buffer.Count)
        {
            uint dataTypeSize = 2;//byte size
            value = (short)(((buffer[(int)(bitIndex / 8)] >> (int)(7 - bitIndex % 8)) & 0x1) == 1 ? 0xFFFF : 0x0);
            for (uint copyBits = 0; copyBits < dataTypeSize * 8; ++copyBits)
            {
                value <<= 1;
                value |= (short)((buffer[(int)((bitIndex + copyBits) / 8)] >> (int)(7 - (bitIndex + copyBits) % 8)) & 0x1);
            }
            bitIndex += dataTypeSize * 8;
        }
        else
        {
            packetErrors++;
            return 0;
        }
        return value;
    }

    //Reads a 32 bit unsigned integer from the buffer
    public uint ReadUnsignedInt()
    {
        uint value;
        if ((int)((bitIndex + 32) / 8) < buffer.Count)
        {
            uint dataTypeSize = 4;//byte size
            value = 0x0;
            for (uint copyBits = 0; copyBits < dataTypeSize * 8; ++copyBits)
            {
                value <<= 1;
                value |= (byte)((buffer[(int)((bitIndex + copyBits) / 8)] >> (int)(7 - (bitIndex + copyBits) % 8)) & 0x1);
            }
            bitIndex += dataTypeSize * 8;
        }
        else
        {
            packetErrors++;
            return 0;
        }
        return value;
    }

    //Reads a 32 bit signed integer from the buffer
    public int ReadSignedInt()
    {
        int value;
        if ((int)((bitIndex + 32) / 8) < buffer.Count)
        {
            uint dataTypeSize = 4;//byte size
            value = (int)(((buffer[(int)(bitIndex / 8)] >> (int)(7 - bitIndex % 8)) & 0x1) == 1 ? 0xFFFFFFFF : 0x0);
            for (uint copyBits = 0; copyBits < dataTypeSize * 8; ++copyBits)
            {
                value <<= 1;
                value |= (buffer[(int)((bitIndex + copyBits) / 8)] >> (int)(7 - (bitIndex + copyBits) % 8)) & 0x1;
            }
            bitIndex += dataTypeSize * 8;
        }
        else
        {
            packetErrors++;
            return 0;
        }
        return value;
    }

    //Reads an n bit custom unsigned integer from the buffer
    public uint ReadCustomUnsignedInteger(uint bits)
    {
        uint value;
        if ((int)((bitIndex + bits) / 8) < buffer.Count)
        {
            value = 0x0;
            for (uint copyBits = 0; copyBits < bits; ++copyBits)
            {
                value <<= 1;
                value |= (uint)((buffer[(int)((bitIndex + copyBits) / 8)] >> (int)(7 - (bitIndex + copyBits) % 8)) & 0x1);
            }
            bitIndex += bits;
        }
        else
        {
            packetErrors++;
            return 0;
        }
        return value;
    }

    //Reads an n bit custom signed integer from the buffer
    public int ReadCustomSignedInteger(uint bits)
    {
        int value;
        if ((int)((bitIndex + bits) / 8) < buffer.Count)
        {
            value = (int)(((buffer[(int)(bitIndex / 8)] >> (int)(7 - bitIndex % 8)) & 0x1) == 1 ? 0xFFFFFFFF : 0x0);
            for (uint copyBits = 0; copyBits < bits; ++copyBits)
            {
                value <<= 1;
                value |= (buffer[(int)((bitIndex + copyBits) / 8)] >> (int)(7 - (bitIndex + copyBits) % 8)) & 0x1;
            }
            bitIndex += bits;
        }
        else
        {
            packetErrors++;
            return 0;
        }
        return value;
    }

    //Reads a 32 bit floating point number from the buffer
    public float ReadFloat()
    {
        if ((int)((bitIndex + 32) / 8) >= buffer.Count)
        {
            packetErrors++;
            return 0;
        }
        return BitConverter.ToSingle(BitConverter.GetBytes(ReadUnsignedInt()), 0);
    }

    //Reads a 64 bit floating point number from the buffer
    public double ReadDouble()
    {
        if ((int)((bitIndex + 32) / 8) >= buffer.Count)
        {
            packetErrors++;
            return 0;
        }
        byte[] bytes = new byte[8];
        byte[] first4bytes = BitConverter.GetBytes(ReadUnsignedInt());
        byte[] last4bytes = BitConverter.GetBytes(ReadUnsignedInt());

        for (uint copyBytes = 0; copyBytes < 4; ++copyBytes)
        {
            bytes[copyBytes] = first4bytes[copyBytes];
        }
        for (uint copyBytes = 4; copyBytes < 8; ++copyBytes)
        {
            bytes[copyBytes] = last4bytes[copyBytes - 4];
        }
        return BitConverter.ToDouble(bytes, 0);
    }

    //Reads a dynamic unsigned integer that ranges in bytes. Has the ability to error out
    public uint ReadDynamicUnsignedInteger(uint arrayBitInterval)
    {
        uint value = 0;
        uint tempValue;
        uint interval = (uint)(0x1 << (int)arrayBitInterval) - 1;
        uint intervalCount = 0;
        uint error;
        for (error = 0; error < 255; ++error)
        {
            if ((int)((bitIndex + arrayBitInterval) / 8) >= buffer.Count)
            {
                packetErrors++;
                return 0;
            }
            tempValue = 0;
            for (uint copyBits = 0; copyBits < arrayBitInterval; ++copyBits)
            {
                tempValue <<= 1;
                tempValue |= (uint)((buffer[(int)((bitIndex + copyBits) / 8)] >> (int)(7 - (bitIndex + copyBits) % 8)) & 0x1);
            }
            bitIndex += arrayBitInterval;
            value += tempValue;
            intervalCount++;
            if (tempValue != interval) break;
        }
        if (error == 256)
        {
            packetErrors++;
        }
        return value;
    }

    //Reads a dynamic signed integer that ranges in bytes. Has the ability to error out
    public int ReadDynamicSignedInteger(uint arrayBitInterval)
    {
        int sign = ReadBoolean() ? 1 : -1;
        int value = (int)ReadDynamicUnsignedInteger(arrayBitInterval);
        return value * sign;
    }

    //Reads the unsigned n bit integer. Has the ability to error out
    public uint ReadDynamicUnsignedNbitInteger(uint bits)
    {
        uint value = 0;
        uint copyBits = 0;
        uint tempValue;
        uint offset = 0;
        uint error;
        for (error = 0; error < 255; ++error)
        {
            if ((int)((bitIndex + copyBits) / 8) >= buffer.Count)
            {
                packetErrors++;
                return 0;
            }
            tempValue = (uint)((buffer[(int)((bitIndex + copyBits) / 8)] >> (int)(7 - (bitIndex + copyBits) % 8)) & 0x1);
            if ((copyBits + 1) % (bits + 1) == 0 && copyBits != 0)
            {
                //trace("-bit-" + copyBits + " " + tempValue);
                offset++;
                //uint nextBit = (uint)((buffer[(int)((bitIndex + copyBits + 1) / 8)] >> (int)(7 - (bitIndex + copyBits + 1) % 8)) & 0x1);
                if (tempValue == 0)
                {
                    break;
                }
            }
            else
            {
                //trace("bit-" + copyBits + " " + tempValue);
                value |= (uint)(((buffer[(int)((bitIndex + copyBits) / 8)] >> (int)(7 - (bitIndex + copyBits) % 8)) & 0x1) << (int)(copyBits - offset));
            }
            copyBits++;
        }
        if (error == 256)
        {
            packetErrors++;
        }
        bitIndex += copyBits + 1;
        return value;
    }

    public uint ReadDynamicUnsignedNbitInteger()
    {
        return ReadDynamicUnsignedNbitInteger(4);
    }

    //Reads the signed n bit integer. Has the ability to error out
    public int ReadDynamicSignedNbitInteger(uint bits)
    {
        int sign = ReadBoolean() ? 1 : -1;
        int value = (int)ReadDynamicUnsignedNbitInteger(bits);
        return value * sign;
    }

    public int ReadDynamicSignedNbitInteger()
    {
        return ReadDynamicSignedNbitInteger(4);
    }

    //Reads a custom resolution float
    public float ReadCustomResolutionFloat(float min, float max, uint bitResolution)
    {
        if ((int)((bitIndex + bitResolution) / 8) >= buffer.Count)
        {
            packetErrors++;
            return 0;
        }
        return ReadCustomUnsignedInteger(bitResolution) / (float)((0x1 << (int)bitResolution) - 1) * (max - min) + min;
    }

    //Reads a custom resolution double
    public double ReadCustomResolutionDouble(double min, double max, uint bitResolution)
    {
        if ((int)((bitIndex + bitResolution) / 8) >= buffer.Count)
        {
            packetErrors++;
            return 0;
        }
        return ReadCustomUnsignedInteger(bitResolution) / (double)((0x1 << (int)bitResolution) - 1) * (max - min) + min;
    }

    /*//Reads a string
    public string ReadString(uint arrayBitInterval)
    {
        string value = string.Empty;
        uint size = ReadDynamicUnsignedNbitInteger(arrayBitInterval);
        if ((int)((bitIndex + 7 * size) / 8) >= buffer.Count)
        {
            packetErrors++;
            return string.Empty;
        }
        for (uint stringItr = 0; stringItr < size; ++stringItr)
        {
            value += (char)(ReadCustomUnsignedInteger(7) + 32);
        }
        return value;
    }
    */

    public string ReadString(uint arrayBitInterval)
    {

        uint size = ReadDynamicUnsignedNbitInteger(arrayBitInterval);

        ASCIIEncoding e = new ASCIIEncoding();

        byte[] data = new byte[size];

        for (int i = 0; i < size; i++)
        {
            data[i] = ReadUnsignedByte();
        }
        return e.GetString(data);
    }

    public string ReadString()
    {
        return ReadString(4);
    }

    //READ FUNCTIONS - ARRAYS

    //Reads an unsigned custom integer array
    public List<uint> ReadUnsignedCustomIntegerArray(uint bits, uint arrayBitInterval)
    {//arrayBitInterval means that for value < 2^arrayBitInterval use the number of bits defined by arrayBitInterval
        List<uint> value = new List<uint>();
        uint size = ReadDynamicUnsignedNbitInteger(arrayBitInterval);
        for (uint cycleArray = 0; cycleArray < size; ++cycleArray)
        {
            value.Add(ReadCustomUnsignedInteger(bits));
        }
        return value;
    }

    //Reads a signed custom integer array
    public List<int> ReadSignedCustomIntegerArray(uint bits, uint arrayBitInterval)
    {//arrayBitInterval means that for value < 2^arrayBitInterval use the number of bits defined by arrayBitInterval
        List<int> value = new List<int>();
        uint size = ReadDynamicUnsignedNbitInteger(arrayBitInterval);
        for (uint cycleArray = 0; cycleArray < size; ++cycleArray)
        {
            value.Add(ReadCustomSignedInteger(bits));
        }
        return value;
    }

    //Reads a float array
    public List<float> ReadFloatArray(uint arrayBitInterval)
    {
        List<float> value = new List<float>();
        uint size = ReadDynamicUnsignedNbitInteger(arrayBitInterval);
        for (uint cycleArray = 0; cycleArray < size; ++cycleArray)
        {
            value.Add(ReadFloat());
        }
        return value;
    }

    //Reads a double array
    public List<double> ReadDoubleArray(uint arrayBitInterval)
    {
        List<double> value = new List<double>();
        uint size = ReadDynamicUnsignedNbitInteger(arrayBitInterval);
        for (uint cycleArray = 0; cycleArray < size; ++cycleArray)
        {
            value.Add(ReadDouble());
        }
        return value;
    }

    //Reads a custom resolution float array
    public List<float> ReadCustomResolutionFloatArray(float min, float max, uint bitResolution, uint arrayBitInterval)
    {
        List<float> value = new List<float>();
        uint size = ReadDynamicUnsignedNbitInteger(arrayBitInterval);
        for (uint cycleArray = 0; cycleArray < size; ++cycleArray)
        {
            value.Add(ReadCustomResolutionFloat(min, max, bitResolution));
        }
        return value;
    }

    //Reads a custom resolution double array
    public List<double> ReadCustomResolutionDoubleArray(double min, double max, uint bitResolution, uint arrayBitInterval)
    {
        List<double> value = new List<double>();
        uint size = ReadDynamicUnsignedNbitInteger(arrayBitInterval);
        for (uint cycleArray = 0; cycleArray < size; ++cycleArray)
        {
            value.Add(ReadCustomResolutionDouble(min, max, bitResolution));
        }
        return value;
    }

    //READ FUNCTIONS - ASSOCIATIVE ARRAYS

    //Reads a string to unsigned integer associative array
    public Dictionary<string, uint> ReadAssociativeArrayStringToUnsignedInteger(uint bits, uint arrayBits)
    {
        Dictionary<string, uint> value = new Dictionary<string, uint>();
        uint size = ReadCustomUnsignedInteger(arrayBits);
        for (uint elementItr = 0; elementItr < size; ++elementItr)
        {
            value.Add(ReadString(5), ReadCustomUnsignedInteger(bits));
        }
        return value;
    }

    //Reads a string to signed integer associative array
    public Dictionary<string, int> ReadAssociativeArrayStringToSignedInteger(uint bits, uint arrayBits)
    {
        Dictionary<string, int> value = new Dictionary<string, int>();
        uint size = ReadCustomUnsignedInteger(arrayBits);
        for (uint elementItr = 0; elementItr < size; ++elementItr)
        {
            value.Add(ReadString(5), ReadCustomSignedInteger(bits));
        }
        return value;
    }

    //Reads a string to float associative array
    public Dictionary<string, float> ReadAssociativeArrayStringToFloat(uint arrayBits)
    {
        Dictionary<string, float> value = new Dictionary<string, float>();
        uint size = ReadCustomUnsignedInteger(arrayBits);
        for (uint elementItr = 0; elementItr < size; ++elementItr)
        {
            value.Add(ReadString(5), ReadFloat());
        }
        return value;
    }

    //Reads a string to double associative array
    public Dictionary<string, double> ReadAssociativeArrayStringToDouble(uint arrayBits)
    {
        Dictionary<string, double> value = new Dictionary<string, double>();
        uint size = ReadCustomUnsignedInteger(arrayBits);
        for (uint elementItr = 0; elementItr < size; ++elementItr)
        {
            value[ReadString(5)] = ReadDouble();
        }
        return value;
    }

    //Reads a string to string associative array
    public Dictionary<string, string> ReadAssociativeArrayStringToString(uint arrayBits)
    {
        Dictionary<string, string> value = new Dictionary<string, string>();
        uint size = ReadCustomUnsignedInteger(arrayBits);
        for (uint elementItr = 0; elementItr < size; ++elementItr)
        {
            value[ReadString(5)] = ReadString(5);
        }
        return value;
    }

    //READ BINARY PACKET

    public Packet ReadFlashPacket()
    {
        Packet packet = new Packet();
        uint packetSize = ReadDynamicUnsignedNbitInteger();
        for (uint copyBytes = 0; copyBytes < packetSize; ++copyBytes)
        {
            packet.WriteUnsignedByte(ReadUnsignedByte());
        }
        bitIndex += packetSize * 8;
        return packet;
    }

    //RESET AND SET BITINDEX
    public void ResetBitIndex() { bitIndex = 0; }
    public void SetBitIndex(uint index) { bitIndex = index; }
    public uint GetBitIndex() { return bitIndex; }

    public uint Length()
    {
        return (uint)buffer.Count;
    }

    public byte[] Retrieve()
    {
        return buffer.ToArray();
    }

    public Boolean CorruptPacket()
    {
        return packetErrors > 0;
    }

    public string Trace()
    {
        string s = string.Empty;
        for (uint copyBits = 0; copyBits < buffer.Count * 8; ++copyBits)
        {
            s += (((buffer[(int)((copyBits) / 8)] >> (int)(7 - (copyBits % 8))) & 0x1) == 0 ? "0" : "1");
        }
        return s;
    }

    /* Useful operation on an int to display the 32-bit binary */
    /*
    string s = string.Empty;
    for(var i:uint = 0; i < 32; ++i){
        s += (((value>>(31-i)) & 0x1) == 0 ? "0" : "1");
    }
    trace(s);
    */


}