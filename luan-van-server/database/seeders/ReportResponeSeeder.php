<?php

namespace Database\Seeders;

use App\Models\ReportRespone;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class ReportResponeSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        ReportRespone::create([
            'content' => "Đang xử lý"
        ]);

        ReportRespone::create([
            'content' => "Không nhận ra vấn đề, không xử lý"
        ]);

        ReportRespone::create([
            'content' => "Đã xử lý, đã xóa nội dung được báo cáo"
        ]);
    }
}
