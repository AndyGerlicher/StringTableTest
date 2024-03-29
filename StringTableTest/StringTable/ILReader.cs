// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reflection;
using System.Reflection.Emit;

namespace System.Diagnostics.Internal
{
    internal class ILReader
    {
        private static readonly OpCode[] singleByteOpCode;
        private static readonly OpCode[] doubleByteOpCode;

        private readonly byte[] _cil;
        private int ptr;


        public ILReader(byte[] cil) => _cil = cil;

        public OpCode OpCode { get; private set; }
        public int MetadataToken { get; private set; }
        public MemberInfo Operand { get; private set; }

        public bool Read(MethodBase methodInfo)
        {
            if (ptr < _cil.Length)
            {
                OpCode = ReadOpCode();
                Operand = ReadOperand(OpCode, methodInfo);
                return true;
            }
            return false;
        }

        OpCode ReadOpCode()
        {
            var instruction = ReadByte();
            if (instruction < 254)
                return singleByteOpCode[instruction];
            else
                return doubleByteOpCode[ReadByte()];
        }

        MemberInfo ReadOperand(OpCode code, MethodBase methodInfo)
        {
            MetadataToken = 0;
            switch (code.OperandType)
            {
                case OperandType.InlineMethod:
                    MetadataToken = ReadInt();
                    Type[] methodArgs = null;
                    if (methodInfo.GetType() != typeof(ConstructorInfo) && !methodInfo.GetType().IsSubclassOf(typeof(ConstructorInfo)))
                    {
                        methodArgs = methodInfo.GetGenericArguments();
                    }
                    Type[] typeArgs = null;
                    if (methodInfo.DeclaringType != null)
                    {
                        typeArgs = methodInfo.DeclaringType.GetGenericArguments();
                    }
                    try
                    {
                        return methodInfo.Module.ResolveMember(MetadataToken, typeArgs, methodArgs);
                    }
#pragma warning disable ERP022 // Can return System.ArgumentException : Token xxx is not a valid MemberInfo token in the scope of module xxx.dll
                    catch
                    {
                        return null;
                    }
#pragma warning restore ERP022
            }
            return null;
        }

        byte ReadByte() => _cil[ptr++];

        int ReadInt()
        {
            var b1 = ReadByte();
            var b2 = ReadByte();
            var b3 = ReadByte();
            var b4 = ReadByte();
            return b1 | b2 << 8 | b3 << 16 | b4 << 24;
        }

#pragma warning disable CA1810 // Initialize reference type static fields inline
        static ILReader()
#pragma warning restore CA1810 // Initialize reference type static fields inline
        {
            singleByteOpCode = new OpCode[225];
            doubleByteOpCode = new OpCode[31];

            var fields = GetOpCodeFields();

            for (var i = 0; i < fields.Length; i++)
            {
                var code = (OpCode)fields[i].GetValue(null);
                if (code.OpCodeType == OpCodeType.Nternal)
                    continue;

                if (code.Size == 1)
                    singleByteOpCode[code.Value] = code;
                else
                    doubleByteOpCode[code.Value & 0xff] = code;
            }
        }

        static FieldInfo[] GetOpCodeFields() => typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static);
    }
}
