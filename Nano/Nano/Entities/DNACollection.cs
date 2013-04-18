using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Nano.Entities
{
	class DNACollection : IEnumerable<DNA>
	{
		public List<SkillDNA> Skills { get; private set; }
		public List<AimSkillDNA> AimSkills { get; private set; }
		public List<AreaSkillDNA> AreaSkills { get; private set; }
		public List<DNA> DNA { get; private set; }
		public Dictionary<SkillDNA, TimeSpan> SkillCooling { get; private set; }
		public DNACollection()
		{
			Skills = new List<SkillDNA>();
			AimSkills = new List<AimSkillDNA>();
			AreaSkills = new List<AreaSkillDNA>();
			DNA = new List<DNA>();
			SkillCooling = new Dictionary<SkillDNA, TimeSpan>();
		}

		public void Add(DNA dna)
		{
			DNA.Add(dna);
			if (dna is SkillDNA) {
				Skills.Add((SkillDNA)dna);
				SkillCooling.Add((SkillDNA)dna, TimeSpan.Zero);
				if (dna is AimSkillDNA) {
					AimSkills.Add((AimSkillDNA)dna);
				}
				if (dna is AreaSkillDNA) {
					AreaSkills.Add((AreaSkillDNA)dna);
				}
			}
		}

		public bool ActivateSkill(SkillDNA skill, LivingEntity activator, Vector2 aim)
		{
			if (SkillCooling[skill] > TimeSpan.Zero)
				return false;

			if(skill.Activate(activator, aim)) {
				SkillCooling[skill] = TimeSpan.FromMilliseconds(skill.Cooldown);
				return true;
			}
			return false;
		}

		public void Update(GameTime gameTime)
		{
			foreach (var key in SkillCooling.Keys.ToArray()) {
				SkillCooling[key] -= gameTime.ElapsedGameTime;
			}
		}

		static Random random = new Random();
		public static DNACollection RandomBlend(DNACollection a, DNACollection b)
		{
			var result = new DNACollection();
			foreach (var dna in a) {
				if (random.NextDouble() > 0.5) {
					result.Add(dna);
				}
			}
			foreach (var dna in b) {
				if (random.NextDouble() > 0.5) {
					result.Add(dna);
				}
			}
			return result;
		}

		public IEnumerator<DNA> GetEnumerator()
		{
			return DNA.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return DNA.GetEnumerator();
		}
	}
}
