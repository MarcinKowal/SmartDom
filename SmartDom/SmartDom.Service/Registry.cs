#region Copyrights
//
// SmartDom
// SmartDom.Service
// Registry.cs
// 
// Created by Marcin Kowal.
// Copyright (c) 2015. All rights reserved.
//  
#endregion

namespace SmartDom.Service
{
    public static class Registry
    {
        public const ushort ID_ADDR = 0;  
        public const ushort TYPE_ADDR = 1; //1 read-only from EEPROM
        public const ushort SUBTYPE_ADDR = 2; //2 read-only from EEPROM
        public const ushort RW_DIG0_ADDR = 3; //3
        public const ushort RW_DIG1_ADDR = 4; //4
        public const ushort RW_DIG2_ADDR = 5; //5
        public const ushort RW_DIG3_ADDR = 6; //6
        public const ushort RW_AN0_ADDR = 7; //6
        public const ushort RW_AN1_ADDR = 8; 
        public const ushort RW_AN2_ADDR = 9;
        public const ushort RW_AN3_ADDR = 10;
        public const ushort LAST_REQUEST_ADDR = 11; //11
        public const ushort TOTAL_ERRORS_ADDR = 12; //12
        public const ushort TOTAL_REGS_SIZE = 13;
    }
}
