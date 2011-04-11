﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpPspEmulator.Core.Cpu.Interpreter
{
    public partial class InterpretedCpu : CpuBase
	{
        public override void SYSCALL(CpuState CpuState)
        {
            uint CODE = CpuState.CODE;
            CpuState.AdvancePC(4);
            CpuState.SystemHle.SyscallHandler.Handle(CpuState, CODE);
            //throw new NotImplementedException();
        }

        public override void CACHE(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void SYNC(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void BREAK(CpuState CpuState)
        {
            //throw new NotImplementedException();
            Console.WriteLine("BREAK");
            CpuState.AdvancePC(4);
        }

        public override void DBREAK(CpuState CpuState)
        {
            throw (new PspDebugBreakException());
            //throw new NotImplementedException();
        }

        public override void HALT(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void DRET(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void ERET(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void MFIC(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void MTIC(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void MFDR(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void MTDR(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void CFC0(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void CTC0(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void MFC0(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void MTC0(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void MFV(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void MFVC(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void MTV(CpuState CpuState)
        {
            throw new NotImplementedException();
        }

        public override void MTVC(CpuState CpuState)
        {
            throw new NotImplementedException();
        }
    }
}
