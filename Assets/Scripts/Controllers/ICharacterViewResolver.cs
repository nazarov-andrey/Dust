using System;
using Dust.Views;
using Dust.Models;

namespace Dust.Controllers
{
	public interface ICharacterViewResolver
	{
		CharacterView Resolve (Character character);
	}
}
