using System;
using UnityEngine;

namespace Rules
{
    public abstract class WinCondition : MonoBehaviour
    {
        public enum Type
        {
            Win,
            Optional,
            Loose
        }


        [SerializeField] private Type m_outcome = Type.Win;
        [SerializeField, Multiline] private string m_description = null;


        public Type Outcome => m_outcome;


        protected void Trigger()
        {
            WinConditionsContainer container = GetComponentInParent<WinConditionsContainer>();
            switch (Outcome)
            {
                case Type.Win:
                    container.Win();
                    break;
                case Type.Loose:
                    container.Loose();
                    break;
                case Type.Optional:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}