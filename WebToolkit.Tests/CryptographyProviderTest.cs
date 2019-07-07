using System;
using System.Text;
using WebToolkit.Common.Providers;
using Xunit;

namespace WebToolkit.Tests
{
    public class CryptographyProviderTest
    {
        [Theory]
        [InlineData("apple")]
        [InlineData("strawberries")]
        [InlineData("I am a test")]
        [InlineData("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas varius condimentum metus sed sagittis. Aenean tincidunt pulvinar interdum. Proin vestibulum, massa et fermentum vehicula, eros nibh ultricies sem, id cursus enim lectus vitae odio. Ut vel tempor magna. Quisque vitae rutrum risus. Fusce vestibulum lacus in quam ultrices, ac volutpat. ")]
        [InlineData(@"colosom Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent dolor augue, mattis non suscipit ut, varius vel arcu. Donec eget felis eros. Nullam a lectus ac nunc egestas porttitor egestas at erat. Nunc finibus ullamcorper erat ac ultrices. Donec consectetur tempus metus, nec aliquam nibh pretium nec. Praesent pellentesque maximus mollis. Nunc semper erat eu viverra convallis. Praesent non mi tempor, ultrices arcu in, placerat orci. Aenean eget neque accumsan, dictum arcu eu, accumsan purus. Curabitur at lectus molestie, ullamcorper velit a, suscipit nulla. Ut luctus placerat faucibus. Integer facilisis luctus dapibus. Aliquam sed sem quis est luctus ultricies.

Ut bibendum tempor bibendum. Aliquam placerat ligula eget dapibus eleifend. Sed aliquam urna ut porta consectetur. Pellentesque luctus felis eu velit vestibulum fermentum. Vivamus nibh urna, fringilla ut leo vel, sagittis dapibus est. Maecenas non sem orci. Mauris quis velit vulputate, ullamcorper velit nec, lobortis turpis. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam fermentum suscipit ante in dapibus. Aenean finibus pharetra nisi, vitae sodales sem lacinia et.

Nulla ac est blandit, malesuada neque nec, mattis nisl. Quisque a urna nunc. Mauris vel elit bibendum, sagittis est id, accumsan diam. Sed rutrum ex ac elit interdum, id ullamcorper eros molestie. Donec eu lorem turpis. Proin consectetur fringilla nulla, et eleifend nisi semper ut. Vivamus egestas urna quis tellus rhoncus, quis maximus justo posuere.

Fusce hendrerit nisl et laoreet auctor. Praesent bibendum felis vel risus ultricies sagittis. Nulla volutpat lobortis magna eget dapibus. Donec mauris ligula, tempor aliquam nisi a, elementum vulputate est. In consectetur ut dolor a interdum. Donec dolor sem, posuere tempus diam nec, bibendum molestie ipsum. Cras eget quam ac nulla suscipit rhoncus a hendrerit tortor. Donec bibendum libero quis consequat ultrices. Sed sed massa nisl. Nullam quis libero efficitur, volutpat ex volutpat, pretium massa. Vivamus rhoncus, elit sit amet fringilla aliquam, risus enim vehicula orci, sed auctor justo leo sed magna. Pellentesque a aliquet velit. Etiam et bibendum libero, nec dapibus erat. In hac habitasse platea dictumst. Nam euismod, dolor ac blandit convallis, metus ligula commodo dolor, ut accumsan tellus magna ac magna. Integer congue metus id scelerisque fringilla.

Donec placerat rutrum sem, ac euismod risus convallis vel. Cras volutpat velit nec libero consectetur accumsan. Vivamus sem est, varius ac ornare nec, finibus eu erat. Etiam sollicitudin magna est, ut imperdiet lorem faucibus at. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Maecenas scelerisque placerat tincidunt. Aenean sem arcu, vulputate at tincidunt vitae, dapibus quis erat. Nunc accumsan auctor diam, et efficitur ipsum pulvinar eget. Nunc id cursus turpis. Aenean dapibus ipsum mi, non tristique lorem faucibus a. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae;

Phasellus in pulvinar diam. Nam faucibus dictum metus, in sagittis est finibus in. Nullam fermentum, nunc vitae placerat dapibus, sapien risus consectetur lectus, eget ultricies felis orci tristique libero. Fusce leo risus, egestas non nisl eu, vehicula tincidunt lorem. In euismod ligula eu volutpat porttitor. Suspendisse potenti. Phasellus mollis tellus at. ")]
        [InlineData("apple@strawberries.com")]
        [InlineData("Peter Apple")]
        public void Do_Encrypt(string data)
        {
            var initialVector = new byte[16];
            var key = new byte[32];
            var rnd = new Random();

            rnd.NextBytes(key);
            rnd.NextBytes(initialVector);

            var sut = new CryptographyProvider();
            var encryptedValue = sut.Encrypt(data, key, initialVector, Encoding.ASCII);
            var value = sut.Decrypt(encryptedValue, key, initialVector, Encoding.ASCII);

            Assert.Equal(data, value);
        }
    }
}