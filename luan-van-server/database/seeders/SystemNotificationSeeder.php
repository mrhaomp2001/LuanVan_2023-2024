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
        ]);

        SystemNotification::create([
            'user_id' => "1",
            "content" => "Thông báo: đã hoàn thành tất cả các chức năng chính của ứng dụng",
        ]);

        SystemNotification::create([
            'user_id' => "1",
            "content" => "Thông báo: đang cập nhật thêm trò chơi mới",
        ]);
    }
}
