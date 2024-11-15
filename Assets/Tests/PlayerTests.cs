using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTests
{
    [Test]
    public void Test_Multiplication()
    {
        // Verifica que 1 * 1 sea igual a 1
        Assert.AreEqual(1, 1 * 1);
    }

    [Test]
    public void Test_Addition()
    {
        // Verifica que 2 + 2 sea igual a 4
        Assert.AreEqual(4, 2 + 2);
    }

    [Test]
    public void Test_Subtraction()
    {
        // Verifica que 3 - 1 sea igual a 2
        Assert.AreEqual(2, 3 - 1);
    }

    // Aquí puedes agregar más pruebas según lo que necesites verificar
}