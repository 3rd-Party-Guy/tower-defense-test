using System;

namespace TDTest.Combat
{
    public class Health
    {
        public event Action<int> OnHealthChange;
        public event Action<int> OnMaxHealthChange;

        public event Action<int> OnHeal;
        public event Action<int> OnDamage;

        public event Action OnDeath;

        public int HP
        {
            get => health;
            private set
            {
                health = value;
                OnHealthChange?.Invoke(health);
            }
        }

        public int MaxHP
        {
            get => maxHealth;
            private set
            {
                maxHealth = value;
                OnMaxHealthChange?.Invoke(maxHealth);
            }
        }

        int health;
        int maxHealth;

        public Health(int Health, int MaxHealth = 0)
        {
            MaxHP = (MaxHealth < Health) ? Health : MaxHealth;
            HP = Health;
        }

        public void Heal(int amount)
        {
            if (HP == maxHealth)
                return;

            HP += amount;

            if (HP > maxHealth)
                HP = maxHealth;

            OnHeal?.Invoke(amount);
        }

        public void Damage(int amount, Action OnDeathCallback = null)
        {
            if (HP <= 0)
                return;

            HP -= amount;

            if (HP <= 0)
            {
                OnEmpty();
                OnDeathCallback?.Invoke();
            }

            OnDamage?.Invoke(amount);
        }

        private void OnEmpty()
        {
            OnDeath?.Invoke();
        }
    }
}
