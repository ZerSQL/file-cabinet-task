using System;
using System.Collections.Generic;
using System.Text;

namespace FileCabinetApp
{
    /// <summary>
    /// Обычная валидация.
    /// </summary>
    public class FileCabinetDefaultService : FileCabinetService
    {
        public FileCabinetDefaultService()
            : base(new DefaultValidator())
        {
        }
    }
}
