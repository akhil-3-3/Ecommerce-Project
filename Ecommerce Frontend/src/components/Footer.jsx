import bg from "../assets/bg.jpg";
import logowhite from "../assets/logowhite.svg";
import fb from "../assets/fb.svg";
import insta from "../assets/insta.svg";
import twitter from "../assets/twitter.svg";
import linkedin from "../assets/linkedin.svg";
import pay1 from "../assets/pay1.svg";
import pay2 from "../assets/pay2.svg";
import pay3 from "../assets/pay3.svg";


const Footer = () => {
  return (
    <footer
      className="relative w-full bg-cover bg-center font-mont"
      style={{ backgroundImage: `url(${bg})` }}
    >
      {/* Dark Overlay */}
      <div className="absolute inset-0 bg-black/70"></div>

      {/* Content */}
      <div className="relative z-10 mx-auto max-w-[1400px] px-10 py-12 text-white">
        <div className="flex justify-between gap-16">
          {/* Left */}
          <div className="max-w-sm">
            <img src={logowhite} alt="Logo" className="mb-6 w-16" />

            <p className="leading-7 text-gray-300">
              Discover a world of exquisite
              <br />
              fragrances and luxury
              <br />
              essentials. Curated for the
              <br />
              discerning individual.
            </p>
          </div>

          {/* Company */}
          <div>
            <h2 className="mb-5 text-xl font-semibold">Company</h2>

            <ul className="space-y-3 text-gray-300">
              <li>About</li>
              <li>Contact</li>
              <li>Terms and Conditions</li>
              <li>Privacy Policy</li>
              <li>Return Policy</li>
              <li>Refund Policy</li>
            </ul>
          </div>

          {/* Newsletter */}
          <div className="max-w-md">
            <h2 className="mb-5 text-xl font-semibold">Newsletter</h2>

            <p className="mb-5 text-gray-300">
              Stay updated with our latest arrivals and offers.
            </p>

            <div className="flex gap-4">
              <input
                type="email"
                placeholder="Enter your email"
                className="flex-1 rounded-lg bg-gray-800 px-4 py-3 outline-none"
              />

              <button className="rounded-full bg-white px-8 py-3 font-medium text-black">
                Subscribe
              </button>
            </div>

            <div className="mt-6 flex gap-6">
              <img src={fb} alt="Facebook" />
              <img src={insta} alt="Instagram" />
              <img src={twitter} alt="Twitter" />
              <img src={linkedin} alt="LinkedIn" />
            </div>
          </div>
        </div>

        {/* Bottom */}
        <div className=" flex mx-auto mt-10 border-t border-white/30 pt-10 text-sm text-gray-300 justify-between">
          <span>© 2025 Dilka Perfumes. All rights reserved.</span>{" "}
          <div className="flex gap-3 items-center">
            <span>Payment Method : </span>
            <img src={pay1} alt="" />
            <img src={pay2} alt="" />
            <img src={pay2} alt="" />
          </div>{" "}
        </div>
      </div>
    </footer>
  );
};

export default Footer;
