/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./Components/**/*.razor",
        "./Components/Pages/**/*.razor",
        "./Components/Layout/**/*.razor",
        "./App.razor"
    ],
    theme: {
        extend: {
            colors: {
                'sentra-navy': '#1E293B',
                'sentra-blue': '#3B82F6',
                'sentra-slate': '#F8FAFC',
            }
        },
    },
    plugins: [],
}