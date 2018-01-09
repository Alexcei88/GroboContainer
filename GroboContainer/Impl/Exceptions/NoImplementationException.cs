using System;

namespace GroboContainer.Impl.Exceptions
{
    public class NoImplementationException : Exception
    {
        public NoImplementationException(Type requested)
            : base(string.Format("��� {0} �� ����� ����������", requested))
        {
        }
    }
}