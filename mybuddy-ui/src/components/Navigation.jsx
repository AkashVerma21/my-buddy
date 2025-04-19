import React, { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { Wallet, LogOut } from 'lucide-react';
import { AuthModal } from './AuthModal';
import '../css/Navigation.css'; // Import the CSS file

export function Navigation() {
  const [isAuthModalOpen, setIsAuthModalOpen] = useState(false);

  return (
    <>
      <nav className="nav-bar">
        <div className="nav-content">
          <div className="nav-header">
            <div className="brand-logo">
              <Wallet className="wallet-icon" />
              <span className="brand-name">MoneyWise</span>
            </div>
            <div className="nav-actions">
              <button
                onClick={() => setIsAuthModalOpen(true)}
                className="login-button"
              >
                Login
              </button>
            </div>
          </div>
        </div>
      </nav>
      <AuthModal isOpen={isAuthModalOpen} onClose={() => setIsAuthModalOpen(false)} />
    </>
  );
}
