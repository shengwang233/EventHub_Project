"use client";

import { useState, useEffect } from "react";

type CurrentUser = {
  userId: string;
  userType: string;
};

export const useCurrentUser = () => {
  const [user, setUser] = useState<CurrentUser | null>(null);

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const res = await fetch("http://localhost:5000/api/account/me", {
          credentials: "include",
        });

        console.log("Me status:", res.status);

        if (res.ok) {
          const data = await res.json();
          setUser({
            userId: data.userId || data.id || "", // 根据你的后端返回字段
            userType: data.userType,
          });
        }
      } catch (error) {
        console.error("Failed to fetch user info", error);
      }
    };

    fetchUser();
  }, []);

  return user;
};
