using System;

namespace GroboContainer.Impl.Abstractions
{
    public class ForbiddenAbstractionException : Exception
    {
        public ForbiddenAbstractionException(Type type) : base(string.Format("��� {0} ��������", type))
        {
        }
    }
}