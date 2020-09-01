using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YGTool.Arquivo
{
    public interface IArquivos
    {
        bool ExportarArquivo();
        bool ImportarArquivo();
    }
}
