using System;
using System.Reflection;

namespace GroboContainer.Impl.Exceptions
{
    public class BadContainerConstructorAttributeException : Exception
    {
        public BadContainerConstructorAttributeException(ConstructorInfo constructor, Type type)
            : base(string.Format(
                       "� ��������� ContainerConstructor ������������ {0} {1} ��� ��������� {2} ������ �������������� �� ����� 1 ����",
                       constructor.ReflectedType, constructor, type))
        {
        }

        public BadContainerConstructorAttributeException(ConstructorInfo constructor)
            : base(string.Format(
                       "����, ��������� � ��������� ContainerConstructor ������������ {0} {1}, �� ������������� ����� ���������� ������������",
                       constructor.ReflectedType, constructor))
        {
        }
    }
}