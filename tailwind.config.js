/** @type {import('tailwindcss').Config} */
module.exports = {
  mode: "jit",
  content: [
    "./src/index.html",
    "./**/*.{fs,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {},
  },
  plugins: [require('@tailwindcss/typography')],
}

