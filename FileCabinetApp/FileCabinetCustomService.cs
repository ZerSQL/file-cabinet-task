using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Измененная валидация.
    /// </summary>
    public class FileCabinetCustomService : FileCabinetService
    {
        public FileCabinetCustomService()
            : base(new CustomValidator())
        {
        }
    }
}
