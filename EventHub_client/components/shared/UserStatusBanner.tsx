
"use client";

import { getCurrentUser } from "@/lib/actions/user.actions";
import { useEffect, useState } from "react";


const UserStatusBanner = () => {
  const [user, setUser] = useState<any>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchUser = async () => {
      const data = await getCurrentUser();
      setUser(data);
      setLoading(false);
    };

    fetchUser();
  }, []);

  if (loading) return <div className="text-sm text-gray-500">Loading user...</div>;

  if (!user) return <div className="text-sm text-red-500">Not logged in</div>;

  return (
    <div className="text-sm text-green-600">
      ✅ Logged in as <strong>{user.firstName} {user.lastName}</strong> ({user.userType})
    </div>
  );
};

export default UserStatusBanner;
