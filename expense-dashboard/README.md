# Expense Dashboard

This project is a React application designed to help users manage their expenses. It provides a user-friendly interface for adding new expenses and displaying existing expenditures.

## Features

- **Add Expense**: Users can add new expenses through a dedicated form.
- **View Expenses**: A list of all added expenses is displayed, showing details such as title, amount, and date.
- **Dashboard**: The main page combines the expense list and the add expense form for easy access.

## Project Structure

```
expense-dashboard
├── public
│   ├── index.html          # Main HTML file
│   └── manifest.json       # Metadata for the application
├── src
│   ├── components          # Reusable components
│   │   ├── AddExpenseForm.tsx  # Form for adding expenses
│   │   ├── ExpenseList.tsx      # Displays list of expenses
│   │   └── ExpenseItem.tsx      # Displays individual expense details
│   ├── pages              # Application pages
│   │   ├── Dashboard.tsx        # Main dashboard page
│   │   └── AddExpense.tsx       # Page for adding a new expense
│   ├── App.tsx            # Main application component
│   ├── index.tsx          # Entry point of the application
│   └── styles             # CSS styles
│       └── App.css       # Styles for the application
├── package.json           # npm configuration file
├── tsconfig.json          # TypeScript configuration file
└── README.md              # Project documentation
```

## Getting Started

1. Clone the repository:
   ```
   git clone <repository-url>
   ```
2. Navigate to the project directory:
   ```
   cd expense-dashboard
   ```
3. Install dependencies:
   ```
   npm install
   ```
4. Start the development server:
   ```
   npm start
   ```

## License

This project is licensed under the MIT License.