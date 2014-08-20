using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Services
{
    public enum ProgramState { Unknown, Valid, Damaged, VersionTooOld, VersionTooNew, ErrorInThisApp }
    public class CheckProgramResult
    {
        public ProgramState State { get; set; }

        public LocalProgramHeader ProgramHeader { get; set; }

        public Program Program { get; set; }
    }


    public interface IProgramValidationService
    {
        Task<CheckProgramResult> CheckProgram(string pathToProgramDirectory);
    }
}
