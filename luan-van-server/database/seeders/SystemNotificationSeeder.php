<?php

namespace Database\Seeders;

use App\Models\SystemNotification;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class SystemNotificationSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        SystemNotification::create([
            'user_id' => "1",
            "content" => "Đây là thông báo thử nghiệm 1",
            "can_use" => true,
        ]);

        SystemNotification::create([
            'user_id' => "1",
            "content" => "Thông báo: đã hoàn thành tất cả các chức năng chính của ứng dụng",
            "can_use" => true,
        ]);

        SystemNotification::create([
            'user_id' => "1",
            "content" => "Thông báo: đang cập nhật thêm trò chơi mới",
            "can_use" => true,
        ]);
    }
}
