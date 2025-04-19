const ExpenseItem = ({ expense }) => {
    return (
        <div className="expense-item">
            <h2>{expense.title}</h2>
            <p>Amount: ${expense.amount}</p>
            <p>Date: {new Date(expense.date).toLocaleDateString()}</p>
        </div>
    );
};

export default ExpenseItem;