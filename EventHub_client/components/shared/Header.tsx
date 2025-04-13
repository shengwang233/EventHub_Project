"use client";
import Image from "next/image";
import Link from "next/link";
import React, { useEffect, useState } from "react";
import { Button } from "../ui/button";
import NavItems from "./NavItems";
import MobileNav from "./MobileNav";

const Header = () => {
  const [userInfo, setUserInfo] = useState<{
    firstName?: string;
    userType?: string;
  } | null>(null);

  useEffect(() => {
    const fetchUserInfo = async () => {
      const res = await fetch("http://localhost:5000/api/account/me", {
        method: "GET",
        credentials: "include",
      });

      if (res.ok) {
        const data = await res.json();
        setUserInfo({ firstName: data.firstName, userType: data.userType });
      } else {
        setUserInfo(null);
      }
    };

    fetchUserInfo();
  }, []);

  const handleLogout = async () => {
    await fetch("http://localhost:5000/api/account/logout", {
      method: "POST",
      credentials: "include",
    });

    localStorage.removeItem("user");
    window.location.href = "/login";
  };

  return (
    <header className="w-full border-b">
      <div className="wrapper flex items-center justify-between">
        <div className="flex items-center w-full max-w-xs">
          <Link href="/" className="flex items-center">
            <Image
              src={"/assets/images/logosvg2.svg"}
              alt={"logo"}
              width={70}
              height={20}
            />
            <span className="text-lg font-semibold ml-1">Events Hub</span>
          </Link>
        </div>

        {/* for desktop navbar*/}

        {(userInfo?.userType === "host" || userInfo?.userType === "admin") && (
          <nav className="md:flex-between hidden w-full max-w-xs">
            <NavItems />
          </nav>
        )}

        <div className="flex items-center gap-4">
          {userInfo ? (
            <>
              <span className="text-sm">Hi, {userInfo.firstName}!</span>
              <Button
                onClick={handleLogout}
                className="rounded-full text-white bg-red-500 hover:bg-red-600"
              >
                Logout
              </Button>
            </>
          ) : (
            <Button asChild className="rounded-full" size={"lg"}>
              <Link href={"/login"}>Login</Link>
            </Button>
          )}

          {/* MobileNav 也需要 userInfo */}
          {/* <MobileNav userInfo={userInfo} /> */}
        </div>
      </div>
    </header>
  );
};

export default Header;
