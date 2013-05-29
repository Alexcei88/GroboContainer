using System;

namespace GroboContainer.Impl.Exceptions
{
    public class AmbiguousConstructorException : Exception
    {
        public AmbiguousConstructorException(Type type) : base(string.Format("���������� ������� ��� {0}", type))
        {
        }
    }
}